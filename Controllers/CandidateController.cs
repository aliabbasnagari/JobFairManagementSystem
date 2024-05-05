using JobFairManagementSystem.CustomAttributes;
using JobFairManagementSystem.Data;
using JobFairManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using RestSharp.Authenticators;

namespace JobFairManagementSystem.Controllers;

public partial class CandidateController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;

    public CandidateController(UserManager<ApplicationUser> userManager,
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

    [Authorize(Roles = UserRoles.CandidateRole)]
    [Verified]
    public IActionResult Home()
    {
        var model = _context.Candidates.Include(c => c.Project).Single(c => c.Id == User.Identity.Name);
       return View(model);
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
    public async Task<IActionResult> RegisterAsync(Candidate model)
    {
        Debug.WriteLine("DEBUG:::::Register");
        if (ModelState.IsValid)
        {
            var roleExists = await _roleManager.RoleExistsAsync(UserRoles.CandidateRole);
            if (roleExists)
            {
                var user = new CandidateUser()
                {
                    Name = model.Name,
                    Email = model.Email,
                    CNIC = model.CNIC,
                    Address = model.Address,
                    Password = model.Password,
                    Gender = model.Gender,
                    ProjectSchedule = new Schedule { Date = DateTime.Now },
                    IsVerified = false
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // if (!roleExists)    await _roleManager.CreateAsync(new IdentityRole(UserRoles.CompanyRole));
                    await _userManager.AddToRoleAsync(user, UserRoles.CandidateRole);
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
        var candidateDb = _context.Candidates.Single(c => c.Id == id);
        return View(candidateDb);
    }

    [HttpPost]
    public IActionResult UpdateAsync(CandidateUser model)
    {
        if (ModelState.IsValid)
        {
            var candidateDb = _context.Candidates.Single(c => c.Id == model.Id);
            candidateDb.Email = model.Email;
            candidateDb.Name = model.Name;
            candidateDb.Bio = model.Bio;
            candidateDb.Address = model.Address;
            candidateDb.CGPA = model.CGPA;
            candidateDb.CNIC = model.CNIC;
            candidateDb.Degree = model.Degree;
            candidateDb.DateOfBirth = model.DateOfBirth;
            candidateDb.Gender = model.Gender;
            candidateDb.GraduationDate = model.GraduationDate;
            candidateDb.Skills = model.Skills;
            candidateDb.SocialLinks = model.SocialLinks;
            candidateDb.Password = model.Password;
            _context.SaveChanges();
        }
        return View(model);
    }

    public IActionResult ListCompanies()
    {
        var companies = _context.Companies
            .Include(c => c.InterviewSchedule)
            .ThenInclude(i => i.Slots)
            .ThenInclude(s => s.User)
            .ToList().FindAll(c => c.IsVerified);

        return View(companies);
    }

    public IActionResult ReserveSlot(string cid, int slotId)
    {
        var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = _context.Candidates.ToList().Find(c => c.Id == uid);

        var company = _context.Companies
            .Include(c => c.InterviewSchedule)
            .ThenInclude(i => i.Slots)
            .ThenInclude(slot => slot.User).Single(c => c.Id == cid);

        var slot = company.InterviewSchedule.Slots.Find(s => s.Id == slotId);
        slot.Reserved = true;
        slot.User = user;
        slot.UserId = uid;
        _context.SaveChanges();
        return RedirectToAction("ListCompanies");
    }

    public IActionResult FreeSlot(int sId)
    {
        var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = _context.Candidates.ToList().Find(c => c.Id == uid);
        var slot = _context.Slots.Include(s => s.User).Single(s => s.Id == sId);
        slot.Reserved = false;
        slot.User = null;
        slot.UserId = null;
        _context.SaveChanges();
        return RedirectToAction("ListCompanies");
    }

    public IActionResult ShowNotifications()
    {
        var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = _context.Users.Include(c => c.Notifications).Single(c => c.Id == uid);
        return View(user.Notifications);
    }

    public IActionResult MarkAsRead(int Id)
    {
        var notification = _context.Notifications.Single(n => n.Id == Id);
        notification.IsRead = true;
        _context.SaveChanges();
        return RedirectToAction("ShowNotifications");
    }

    public IActionResult AddSkill(string Skill)
    {
        var user = _context.Candidates.Single(c => c.Id == User.Identity.Name);
        user.Skills ??= [];
        user.Skills.Add(Skill);
        _context.SaveChanges();
        return RedirectToAction("Home");
    }

    public IActionResult AddSocialLink(string SLink)
    {
        var user = _context.Candidates.Single(c => c.Id == User.Identity.Name);
        user.SocialLinks ??= [];
        user.SocialLinks.Add(SLink);
        _context.SaveChanges();
        return RedirectToAction("Home");
    }

    public IActionResult UpdateProject(Project model)
    {
        var user = _context.Candidates.Include(c => c.Project).Single(c => c.Id == User.Identity.Name);
        user.Project ??= new Project();
        user.Project.Title = model.Title;
        user.Project.Description = model.Description;
        _context.SaveChanges();
        return RedirectToAction("Home");
    }

    public async Task<IActionResult> CreateSchedule()
    {
        var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var sch = _context.Candidates
            .Include(c => c.ProjectSchedule)
            .ThenInclude(i => i.Slots) // Eager loading of Slots
            .ThenInclude(s => s.User)
            .Single(c => c.Id == uid)
            .ProjectSchedule;

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
            _context.ProjectSchedules.Include(i => i.Slots);

            var sch = _context.Candidates
                .Include(c => c.ProjectSchedule)
                .ThenInclude(i => i.Slots) // Eager loading of Slots
                .Single(c => c.Id == user.Id)
                .ProjectSchedule;

            sch.Slots.Add(model.Slot);
            await _context.SaveChangesAsync();
            return RedirectToAction("CreateSchedule");
        }
        return View("CreateSchedule", model);
    }

    public async Task<IActionResult> DeleteSlot(int id)
    {
        var slotToDelete = _context.Slots.Include(s => s.User).Single(s => s.Id == id);
        _context.Slots.Remove(slotToDelete);
        await _context.SaveChangesAsync();
        return RedirectToAction("CreateSchedule");
    }

    public IActionResult SendInterviewRemainder(string rid, string slot)
    {
        var user = _context.Users.Include(u => u.Notifications).Single(u => u.Id == rid);
        user.Notifications.Add(new Notification(User.Identity.Name, rid, "IMPORTANT! You have interview with us at " + slot));
        _context.SaveChanges();
        return RedirectToAction("CreateSchedule");
    }
}