using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LetsPaint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LetsPaint.BusinessAccess;
using LetsPaint.BusinessAccess.Common;
using LetsPaint.BusinessAccess.Auth;
using LetsPaint.ModelAccess.Auth;

namespace LetsPaint.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Signup()
        {
            DropdownData dropdownData = new DropdownData();
            return View(dropdownData.Get("usertype"));
        }
        [HttpPost]
        public IActionResult SaveSignup(SignupModel signupModel)
        {
            if(ModelState.IsValid)
            {
                SignupDetails signupDetails = new SignupDetails();
                var userdata = signupDetails.SaveSignUp(signupModel);
                if (userdata.UserId > 0)
                {
                    signupModel.UserId = userdata.UserId;
                    return View("CompleteProfile", userdata);
                }
                else
                {
                    DropdownData dropdownData = new DropdownData();
                    return View("Signup", dropdownData.Get("usertype"));
                }
            }
            else
            {
                DropdownData dropdownData = new DropdownData();
                return View("Signup", dropdownData.Get("usertype"));
            }
        }

        [HttpGet]
        public IActionResult CompleteProfile()
        {
            DropdownData dropdownData = new DropdownData();
            return View(dropdownData.Get("usertype"));
        }

        [HttpPost]
        public IActionResult SaveProfile([FromForm] UserProfileModel userProfileModel)
        {
            SignupDetails signupDetails = new SignupDetails();
            if (signupDetails.SaveProfile(userProfileModel))
            {
                DropdownData dropdownData = new DropdownData();
                return View("~/views/home/index.cshtml");
            }
            else
            {
                return View();
            }
        }
    }
}