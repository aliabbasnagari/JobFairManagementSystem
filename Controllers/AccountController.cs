using JobFairManagementSystem.Data;
using JobFairManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JobFairManagementSystem.Controllers;

public class AccountController : Controller
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    // GET
    public async Task<IActionResult> IndexAsync()
    {
        if (_signInManager.IsSignedIn(User))
        {
            var user = await _userManager.GetUserAsync(User);
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains(UserRoles.AdminRole)) return RedirectToAction("Home", "Admin");
            if (userRoles.Contains(UserRoles.CompanyRole)) return RedirectToAction("Home", "Company");
            if (userRoles.Contains(UserRoles.CandidateRole)) return RedirectToAction("Home", "Candidate");
        }
        return View("Login");
    }

    public IActionResult Login()
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
            if (result.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Contains(UserRoles.AdminRole))
                {
                    return RedirectToAction("Home", "Admin");
                }
                else if (userRoles.Contains(UserRoles.CompanyRole))
                {
                    return RedirectToAction("Home", "Company");
                }
                else if (userRoles.Contains(UserRoles.CandidateRole))
                {
                    return RedirectToAction("Home", "Candidate");
                }
            };

        }
        return View("Login", model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult VerificationPending()
    {
        return View();
    }
}