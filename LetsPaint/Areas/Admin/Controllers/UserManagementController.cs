using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LetsPaint.BusinessAccess.Admin.UserManagement;
using LetsPaint.ModelAccess.Admin.UserManagement;
using LetsPaint.ModelAccess.Common;
using Microsoft.AspNetCore.Mvc;

namespace LetsPaint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class UserManagementController : Controller
    {
        private UserManagementDetails _userManagementDetails = null;
        public UserManagementController()
        {
            _userManagementDetails = new UserManagementDetails();
        }

        [HttpGet]
        [Route("UserList")]
        public IActionResult UserList()
        {           
            return View();
        }

        [HttpPost]
        [Route("GetUserList")]
        public JsonResult GetUserList([FromBody] PagingInModel pagingInModel)
        {
            return Json(_userManagementDetails.GetUserList(pagingInModel));
        }

        [HttpPost]
        [Route("BlockUser")]
        public JsonResult BlockUser([FromBody] UserBlockModel userBlockModel)
        {
            return Json(_userManagementDetails.BlockUser(userBlockModel));
        }
        [HttpPost]
        [Route("DeleteUser")]
        public JsonResult BlockUser([FromQuery] int UserId)
        {
            return Json(_userManagementDetails.DeleteUser(UserId));
        }
    }
}