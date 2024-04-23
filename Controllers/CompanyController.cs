using System.Diagnostics;
using JobFairManagementSystem.Data;
using JobFairManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobFairManagementSystem.Controllers
{
    public class CompanyController : Controller
    {
        private ApplicationDbContext context { get; set; }
        public CompanyController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            Debug.WriteLine("REGISTER");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Company company)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", company);
            }

            if (!company.Password.Equals(company.ConfirmPassword))
            {
                ModelState.AddModelError("ConfirmPassword", "Password do not match.");
                return View("Register", company);
            }

            context.Companies.Add(company);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }
    }
}
