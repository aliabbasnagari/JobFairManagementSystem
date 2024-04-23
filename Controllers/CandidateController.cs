using Microsoft.AspNetCore.Mvc;

namespace JobFairManagementSystem.Controllers
{
    public class CandidateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListRecruiters()
        {
            return View();
        }


    }
}
