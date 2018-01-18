using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Services;

namespace SchoolManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/Home/[action]")]
    public class HomeController : Controller
    {
        private IBasicService _basicService;

        public HomeController(BasicService basicService)
        {
            _basicService = basicService;
        }
        public IActionResult Index()
        {
            return Ok("This is Home");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
