using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MayaMaker.Services.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        public List<string> Messages { get; set; }

        private MessageMakerController controller;

        public HomeController(MessageMakerController messageMakerController)
        {
            controller = messageMakerController;
        }

        // GET: MessageView
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetScenario")]
        public async Task<IActionResult> GetScenario()
        {
            var output = await controller.Get();
            return View("Index", output);
        }

        [HttpGet("GetAllScenario")]
        public async Task<IActionResult> GetAllScenario()
        {
            var output = await controller.GetAll();
            return View("Index", output);
        }
    }
}