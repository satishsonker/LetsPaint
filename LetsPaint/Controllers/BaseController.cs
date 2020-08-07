using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LetsPaint.BusinessAccess.Common;
using Microsoft.AspNetCore.Mvc;

namespace LetsPaint.Controllers
{
    [Route("[controller]")]
    public class BaseController : Controller
    {
        [HttpGet]
        [Route("GetDropdownData")]
        public JsonResult GetDropdownData([FromQuery] string key)
        {
            DropdownData dropdownData = new DropdownData();
            return Json(dropdownData.Get(key));

        }

        [HttpGet]
        [Route("GetRefLookupData")]
        public JsonResult GetRefLookupData([FromQuery] string key)
        {
            RefLookupData refLookupData = new RefLookupData();
            return Json(refLookupData.Get(key.Split(",").ToList()));

        }
    }
}