using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Client.Controllers
{
    public class Home : Controller
    {
        // private readonly ILogger<Home> _logger;

        public Home(ILogger<Home> logger)
        {
            // _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.IsResult = false;
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            if (file != null)
            {
                byte[] fileBytes = null;
                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    fileBytes = binaryReader.ReadBytes((int)file.Length);
                }
                string text = Encoding.UTF8.GetString(fileBytes);

                ViewBag.Text = text;
                ViewBag.IsResult = true;

                return View();
            }
            ViewBag.IsResult = false;
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }
    }
}
