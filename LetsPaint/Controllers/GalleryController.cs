using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LetsPaint.BusinessAccess.Gallery;

namespace LetsPaint.Controllers
{
    public class GalleryController : Controller
    {
        [HttpGet]
        public JsonResult GetGalleryType()
        {
            GalleryDetails galleryDetails = new GalleryDetails();
            return Json(galleryDetails.GetGalleryType());
        }

        [HttpGet]
        public JsonResult GetGallery([FromQuery] int galleryTypeId)
        {
            GalleryDetails galleryDetails = new GalleryDetails();
            return Json(galleryDetails.GetGallery(galleryTypeId));
        }
    }
}