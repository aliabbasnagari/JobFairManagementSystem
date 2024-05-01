using JobFairManagementSystem.CustomAttributes;
using JobFairManagementSystem.Data;
using JobFairManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> HomeAsync()
    {
        var model = await _userManager.GetUserAsync(User);
        if (model == null) return View("Login");
        return View((CandidateUser)model);
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
            .ThenInclude(s => s.Candidate)
            .ToList().FindAll(c => c.IsVerified);

        return View(companies);
    }

    public async Task<IActionResult> ReserveSlot(string cid, int slotId)
    {
        var user = await _userManager.GetUserAsync(User);

        var company = _context.Companies
            .Include(c => c.InterviewSchedule)
            .ThenInclude(i => i.Slots)
            .ThenInclude(slot => slot.Candidate).Single(c => c.Id == cid);

        var slot = company.InterviewSchedule.Slots.Find(s => s.Id == slotId);
        slot.Reserved = true;
        slot.Candidate = (CandidateUser)user;
        slot.CandidateId = user.Id;
        await _context.SaveChangesAsync();
        return RedirectToAction("ListCompanies");
    }
}