using JobFairManagementSystem.Data;
using JobFairManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
                    InterviewSchedule = new InterviewSchedule { Date = DateTime.Now },
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
        var user = await _userManager.GetUserAsync(User);
        var sch = _context.Companies
            .Include(c => c.InterviewSchedule)
            .ThenInclude(i => i.Slots) // Eager loading of Slots
            .ThenInclude(s => s.Candidate)
            .Single(c => c.Id == user.Id)
            .InterviewSchedule;

        CreateScheduleVM model = new CreateScheduleVM()
        {
            Slot = new Slot
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now
            },
            InterviewSchedule = sch
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
        var user = await _userManager.GetUserAsync(User);
        _context.InterviewSchedules.Include(i => i.Slots);
        var sch = _context.Companies
            .Include(c => c.InterviewSchedule)
            .ThenInclude(i => i.Slots) // Eager loading of Slots
            .Single(c => c.Id == user.Id)
            .InterviewSchedule;

        if (sch == null) return RedirectToAction("CreateSchedule");
        var slotToDelete = sch.Slots.FirstOrDefault(slot => slot.Id == id);
        if (slotToDelete == null) return RedirectToAction("CreateSchedule");
        sch.Slots.Remove(slotToDelete);
        await _context.SaveChangesAsync();
        return RedirectToAction("CreateSchedule");
    }
}