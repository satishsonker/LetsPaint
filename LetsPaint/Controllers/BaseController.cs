using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LetsPaint.BusinessAccess.Common;
using Microsoft.AspNetCore.Mvc;

namespace LetsPaint.Controllers
{
    public class BaseController : Controller
    {
        [HttpGet]
        [Route("GetDropdownData")]
        public JsonResult GetDropdownData([FromQuery] string key)
        {
            DropdownData dropdownData = new DropdownData();
            return Json(dropdownData.Get(key));

        }
    }
}