using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Arcturus.MvcUI.Models;
using Arcturus.Commands.UsersCmds;
using TasqR;

namespace Arcturus.MvcUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ITasqR tasq;

        public HomeController(ILogger<HomeController> logger,
            ITasqR tasq)
        {
            this.logger = logger;
            this.tasq = tasq;
        }

        public IActionResult Index()
        {
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


        public async Task<IActionResult> Register()
        {
            var register = new NewUserCmd("Gigi", 
                null,
                "", 
                Domain.Gender.Female);

            return Ok(await tasq.RunAsync(register));
        }
    }
}
