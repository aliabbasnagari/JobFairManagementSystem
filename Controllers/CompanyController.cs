using JobFairManagementSystem.Data;
using JobFairManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using JobFairManagementSystem.CustomAttributes;
using Microsoft.AspNetCore.Authorization;

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
    public async Task<IActionResult> Login(LoginVM model)
    {
        Debug.WriteLine("DEBUG:::::Login " + model.Email);

        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return View("Login", model);

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded) return RedirectToAction("Home");

        }
        return View("Login", model);
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

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index");
    }
}