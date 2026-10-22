using WDPAssessment3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography.Pkcs;
using WDPAssessment3.Data;
using Microsoft.EntityFrameworkCore;

namespace WDPAssessment3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Images = await _context.AIImage.ToListAsync();
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Email = "u3241255@uni.canberra.edu.au";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
