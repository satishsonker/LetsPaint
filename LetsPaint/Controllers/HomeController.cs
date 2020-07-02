using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LetsPaint.Models;
using LetsPaint.ModelAccess.Common;
using Newtonsoft.Json;

namespace LetsPaint.Controllers
{
    public class HomeController : Controller
    {       
        public IActionResult Index()
        {
            HttpContext.Session.SetObjectAsJson("test", "asd");
            var loginData = TempData["loginData"];
            if (loginData != null)
            {
                ApiResponseModel tenant =JsonConvert.DeserializeObject<ApiResponseModel>(TempData["loginData"].ToString());
            }
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
 public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Services()
        {
            return View();
        }
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
