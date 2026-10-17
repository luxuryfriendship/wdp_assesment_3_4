using AspNetCoreWebSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography.Pkcs;

namespace AspNetCoreWebSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var assignment1Mark = 30;
            var assignment2Mark = 30;
            var tutorialMark = 30;
            var totalMark = assignment1Mark + assignment2Mark + tutorialMark;
            ViewBag.MyTotalMark = totalMark;

            return View();
        }

        public IActionResult Privacy()
        {
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
