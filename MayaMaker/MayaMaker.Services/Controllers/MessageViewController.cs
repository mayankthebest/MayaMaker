using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MayaMaker.Services.Controllers
{
    [Route("[controller]")]
    public class MessageViewController : Controller
    {
        // GET: MessageView
        public IActionResult Index()
        {
            //ViewData["Title"] = "";
            return View();
        }
    }
}