using JobFairManagementSystem.Data;
using JobFairManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JobFairManagementSystem.Controllers;

public class AccountController : Controller
{
    // GET
    public IActionResult Login()
    {
        return RedirectToAction("Login", "Company");
    }
}