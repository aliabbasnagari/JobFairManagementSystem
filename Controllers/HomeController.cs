using JobFairManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using JobFairManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JobFairManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            await _signInManager.SignOutAsync();
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {

                }
                if (user != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    if (userRoles.Contains(UserRoles.AdminRole)) return RedirectToAction("Home", "Admin");
                    if (userRoles.Contains(UserRoles.CompanyRole)) return RedirectToAction("Home", "Company");
                    if (userRoles.Contains(UserRoles.CandidateRole)) return RedirectToAction("Home", "Candidate");
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
