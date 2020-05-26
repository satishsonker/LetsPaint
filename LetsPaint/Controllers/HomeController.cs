using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LetsPaint.Models;

namespace LetsPaint.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PaintingGallery()
        {
            return View();
        }
        public IActionResult CraftGallery()
        {
            return View();
        }

        public IActionResult RangoliGallery()
        {
            return View();
        }

        public IActionResult WallPaintingGallery()
        {
            return View();
        }

        public IActionResult TheArtist()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
