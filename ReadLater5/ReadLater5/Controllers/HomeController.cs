using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReadLater5.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ReadLater5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClickService _clickService;
        private readonly IBookmarkService _bookmarkService;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> MostClicked()
        {
            var clickVm = await _clickService.GetMostPopularClicks();
            var orderedList = clickVm.OrderByDescending(x => x.TotalClicks).Take(5);
            return PartialView("~/Views/Shared/_MostClicked.cshtml", orderedList);
        }

        [HttpGet]
        public async Task<ActionResult> MostClickedToday()
        {
            var clickVm = await _clickService.GetMostPopularClicksToday();
            var orderedList = clickVm.OrderByDescending(x => x.TotalClicks).Take(5);
            return PartialView("~/Views/Shared/_TopFiveToday.cshtml", orderedList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}