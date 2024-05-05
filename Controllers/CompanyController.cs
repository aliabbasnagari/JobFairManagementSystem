using JobFairManagementSystem.Data;
using JobFairManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using JobFairManagementSystem.CustomAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace JobFairManagementSystem.Controllers;

public class CompanyController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;

    public CompanyController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _context = context;

    }


    // GET
    public IActionResult Index()
    {
        if (_signInManager.IsSignedIn(User)) return RedirectToAction("Home");
        return View("Login");
    }

    [Authorize(Roles = UserRoles.CompanyRole)]
    [Verified]
    public async Task<IActionResult> HomeAsync()
    {
        var model = await _userManager.GetUserAsync(User);
        if (model == null) return View("Login");
        return View((CompanyUser)model);
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> RegisterAsync(Company model)
    {
        Debug.WriteLine("DEBUG:::::Register");
        if (ModelState.IsValid)
        {
            var roleExists = await _roleManager.RoleExistsAsync(UserRoles.CompanyRole);
            if (roleExists)
            {
                var user = new CompanyUser()
                {
                    Name = model.Name,
                    Address = model.Address,
                    Email = model.Email,
                    Password = model.Password,
                    ContactEmail = model.Email,
                    Description = model.Description,
                    InterviewSchedule = new Schedule { Date = DateTime.Now },
                    IsVerified = false
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // if (!roleExists)    await _roleManager.CreateAsync(new IdentityRole(UserRoles.CompanyRole));
                    await _userManager.AddToRoleAsync(user, UserRoles.CompanyRole);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    Debug.WriteLine("DEBUG:::::Register-" + error.Description);
                }
            }
        }
        return View("Register", model);
    }

    public IActionResult UpdateAsync(string id)
    {
        var companyDb = _context.Companies.Single(c => c.Id == id);
        return View(companyDb);
    }

    [HttpPost]
    public IActionResult UpdateAsync(CompanyUser model)
    {
        if (ModelState.IsValid)
        {
            var companyDb = _context.Companies.Single(c => c.Id == model.Id);
            companyDb.Description = model.Description;
            companyDb.Address = model.Address;
            companyDb.Email = model.Email;
            companyDb.ContactEmail = model.Email;
            companyDb.Name = model.Name;
            companyDb.Password = model.Password;
            _context.SaveChanges();
        }
        return View(model);
    }

    public async Task<IActionResult> CreateSchedule()
    {
        var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var sch = _context.Companies
            .Include(c => c.InterviewSchedule)
            .ThenInclude(i => i.Slots) // Eager loading of Slots
            .ThenInclude(s => s.User)
            .Single(c => c.Id == uid)
            .InterviewSchedule;

        var model = new CreateScheduleVM()
        {
            Schedule = sch
        };
        return View(model);
    }

    public async Task<IActionResult> AddSlot(CreateScheduleVM model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);
            _context.InterviewSchedules.Include(i => i.Slots);
            var sch = _context.Companies
                .Include(c => c.InterviewSchedule)
                .ThenInclude(i => i.Slots) // Eager loading of Slots
                .Single(c => c.Id == user.Id)
                .InterviewSchedule;

            sch.Slots.Add(model.Slot);
            await _context.SaveChangesAsync();
            return RedirectToAction("CreateSchedule");
        }
        return View("CreateSchedule", model);
    }

    public async Task<IActionResult> DeleteSlot(int id)
    {
        var slotToDelete = _context.Slots.Include(s => s.User).Single(s => s.Id == id);
        if (slotToDelete.User != null)
        {
            _context.Notifications.Add(new Notification(User.Identity.Name, slotToDelete.User.Id, "The slot " + slotToDelete + " you reserved has been deleted!"));
        }
        _context.Slots.Remove(slotToDelete);
        await _context.SaveChangesAsync();
        return RedirectToAction("CreateSchedule");
    }

    public IActionResult SendInterviewRemainder(string rid, string slot)
    {
        _context.Notifications.Add(new Notification(User.Identity.Name, rid, "IMPORTANT! You have interview with us at " + slot));
        _context.SaveChanges();
        return RedirectToAction("CreateSchedule");
    }

    public IActionResult ListProjects()
    {
        var candidates = _context.Candidates
            .Include(candidateUser => candidateUser.Project)
            .Include(c => c.ProjectSchedule)
            .ThenInclude(p => p.Slots).ToList()
            .FindAll(c => c.Project != null);
        return View(candidates);
    }

    public IActionResult ReserveSlot(string cid, int slotId)
    {
        var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var company = _context.Companies.ToList().Find(c => c.Id == uid);

        var candidate = _context.Candidates
            .Include(c => c.ProjectSchedule)
            .ThenInclude(p => p.Slots)
            .ThenInclude(slot => slot.User).Single(c => c.Id == cid);


        _context.Notifications.Add(new Notification(uid, candidate.Id, "Company " + company.Name + " reserved a slot to view your Project."));
        var slot = candidate.ProjectSchedule.Slots.Find(s => s.Id == slotId);
        slot.Reserved = true;
        slot.User = company;
        slot.UserId = uid;
        _context.SaveChanges();
        return RedirectToAction("ListProjects");
    }

    public IActionResult FreeSlot(int sId)
    {
        var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var company = _context.Companies.Single(c => c.Id == uid);
        var slot = _context.Slots.Include(s => s.User).Single(s => s.Id == sId);
        _context.Notifications.Add(new Notification(uid, slot.User.Id, "Company " + company.Name + " Freed their slot to view your Project."));
        slot.Reserved = false;
        slot.User = null;
        slot.UserId = null;
        _context.SaveChanges();
        return RedirectToAction("ListProjects");
    }

    public IActionResult ShowFeedback()
    {
        return View();
    }

    public IActionResult GiveFeedback()
    {
        Feedback feedback = new Feedback
        {
            FromUserId = User.Identity.Name
        };
        return View(feedback);
    }

    [HttpPost]
    public IActionResult GiveFeedback(Feedback model)
    {
        var toUser = _context.Users.Single(u => u.Id == model.ToUserId);
        var fromUser = _context.Users.Single(u => u.Id == model.FromUserId);
        _context.Feedbacks.Add(new Feedback
        {
            FromUser = (model.Anonymous) ? null : fromUser,
            FromUserId = (model.Anonymous) ? null : model.FromUserId,
            ToUserId = model.ToUserId,
            ToUser = toUser,
            Message = model.Message
        });
        _context.SaveChanges();
        return RedirectToAction("GiveFeedback");
    }
}