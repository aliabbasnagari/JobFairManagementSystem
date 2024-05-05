using AutoMapper;
using JobFairManagementSystem.CustomAttributes;
using JobFairManagementSystem.Data;
using JobFairManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JobFairManagementSystem.Controllers;

public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;

    public AdminController(UserManager<ApplicationUser> userManager,
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
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }


    [Authorize(Roles = UserRoles.AdminRole)]
    [Verified]
    public async Task<IActionResult> HomeAsync()
    {
        var model = await _userManager.GetUserAsync(User);
        if (model == null) return View("Login");
        return View((AdminUser)model);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAsync(Admin model)
    {
        Debug.WriteLine("DEBUG:::::Register");
        if (ModelState.IsValid)
        {
            var roleExists = await _roleManager.RoleExistsAsync(UserRoles.AdminRole);
            if (roleExists)
            {
                var user = new AdminUser()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    CNIC = model.CNIC,
                    PhoneNumber = model.PhoneNumber,
                    IsVerified = false
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.AdminRole);
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

    public IActionResult VerifyCompanies()
    {
        var companies = _context.Companies.ToList();
        return View(companies);
    }

    public IActionResult VerifyCandidates()
    {
        var candidates = _context.Candidates.ToList();
        return View(candidates);
    }

    public IActionResult VerifyUser(string id, string actionName)
    {
        var user = _context.Users.Single(u => u.Id == id);
        user.IsVerified = !user.IsVerified;
        _context.SaveChanges();
        return RedirectToAction(actionName);
    }

    public IActionResult AssignVenues()
    {
        var companies = _context.Companies.ToList().FindAll(c => c.IsVerified);
        return View(companies);
    }

    [HttpPost]
    public IActionResult AssignVenue(string id, string venue)
    {
        var company = _context.Companies.Single(c => c.Id == id);
        company.Venue = venue;
         _context.SaveChanges();
        return RedirectToAction("AssignVenues");
    }


    public IActionResult SendNotification()
    {
        Notification model = new Notification
        {
            ApplicationUserId = "ALL",
            SenderId = User.Identity.Name
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult SendNotification(Notification model)
    {
        if (ModelState.IsValid)
        {
            if (model.ApplicationUserId.Equals("ALL"))
            {
                foreach (var user in _context.Users.Include(u => u.Notifications).ToList()
                             .FindAll(u => u.Id != model.SenderId))
                {
                    user.Notifications.Add(new Notification(model.SenderId, user.Id, model.Message));
                }
            }
            else
            {
                var user = _context.Users.Include(u => u.Notifications).ToList().Single(u => u.Id == model.ApplicationUserId);
                user.Notifications.Add(new Notification(model.SenderId, user.Id, model.Message));
            }

            _context.SaveChanges();
            return RedirectToAction("SendNotification");
        }

        model.ApplicationUserId = "ALL";
        return View("SendNotification", model);
    }

    public IActionResult SendInvititation()
    {
        List<string> participants = [];
        return View(participants);
    }
}