using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LetsPaint.BusinessAccess.Admin.GalleryManagement;
using LetsPaint.Controllers;
using LetsPaint.ModelAccess.Admin.GalleryManagement;
using LetsPaint.ModelAccess.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LetsPaint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class GalleryManagementController : BaseController
    {
       private readonly GalleryManagementDetails galleryManagementDetails;
        public GalleryManagementController(IHostingEnvironment hostingEnvironment)
        {
           galleryManagementDetails=new GalleryManagementDetails(hostingEnvironment);
        }

        [HttpGet]
        [Route("GalleryType")]
        public IActionResult GalleryType()
        {
            return View();
        }
        [HttpGet]
        [Route("AddGalleryType")]
        public IActionResult AddGalleryType()
        {
            return View();
        }
        [HttpPost]
        [Route("GetGalleryType")]
        public JsonResult GetGalleryType([FromBody] PagingInModel pagingInModel)
        {
            return Json(galleryManagementDetails.GetGalleryType(pagingInModel));

        }

        [HttpPost]
        [Route("SaveGalleryType")]
        public JsonResult SaveGalleryType([FromBody] GalleryTypeModel galleryTypeModel)
        {
            return Json(galleryManagementDetails.SaveGalleryType(galleryTypeModel));

        }

        [HttpPost]
        [Route("UpdateGalleryType")]
        public JsonResult UpdateGalleryType([FromBody] GalleryTypeModel galleryTypeModel)
        {
            return Json(galleryManagementDetails.UpdateGalleryType(galleryTypeModel));

        }

        [HttpPost]
        [Route("DeleteGalleryType")]
        public JsonResult DeleteGalleryType([FromBody] GalleryTypeModel galleryTypeModel)
        {
            return Json(galleryManagementDetails.DeleteGalleryType(galleryTypeModel));
        }

        [HttpGet]
        [Route("Gallery")]
        public IActionResult Gallery()
        {
            return View();
        }

        [HttpGet]
        [Route("AddGallery")]
        public IActionResult AddGallery()
        {
            return View();
        }

        [HttpGet]
        [HttpPost]
        [Route("GetGallery")]
        public JsonResult GetGallery([FromBody] PagingInModel pagingInModel)
        {
            return Json(galleryManagementDetails.GetGallery(pagingInModel));
        }

        [HttpPost]
        [Route("SaveGallery")]
        public JsonResult SaveGallery([FromForm] GalleryModel galleryModel)
        {
            return Json(galleryManagementDetails.SaveGallery(galleryModel));
        }

        [HttpPost]
        [Route("UpdateGallery")]
        public JsonResult UpdateGallery([FromForm] GalleryModel galleryModel)
        {
            return Json(galleryManagementDetails.UpdateGallery(galleryModel));
        }

        [HttpPost]
        [Route("DeleteGallery")]
        public JsonResult DeleteGallery([FromBody] GalleryModel galleryModel)
        {
            return Json(galleryManagementDetails.DeleteGallery(galleryModel));
        }

        [HttpPost]
        [Route("DeleteGalleryImage")]
        public JsonResult DeleteGalleryImage([FromBody] GalleryImageDeleteModel galleryImageDeleteModel)
        {
            return Json(galleryManagementDetails.DeleteGalleryImage(galleryImageDeleteModel));
        }
    }
}