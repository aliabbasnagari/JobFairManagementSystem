using AutoMapper;
using JobFairManagementSystem.CustomAttributes;
using JobFairManagementSystem.Data;
using JobFairManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
        Debug.WriteLine(id);
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

}