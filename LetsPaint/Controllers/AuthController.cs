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
using Newtonsoft.Json;
using LetsPaint.ModelAccess.Common;
using LetsPaint.Filters;
using LetsPaint.ModelAccess.Models;

namespace LetsPaint.Controllers
{

    public class AuthController : Controller
    {
        private readonly IEmailSender _emailSender;
        public AuthController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        [HttpPost]
        public IActionResult Login([FromForm] LoginViewModel loginViewModel)
        {

            LoginDetails loginDetails = new LoginDetails();
            var _output = loginDetails.Login(loginViewModel);
            if (_output.Data != null)
            {
                HttpContext.Session.SetObjectAsJson("loginData", _output.Data);
                return View("/views/home/index.cshtml", loginDetails.Login(loginViewModel));
            }
            else
            {
                TempData["Error"] = _output.Message;
                return View("login");
            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {            
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword([FromForm] string Username)
        {
            SignupDetails signupDetails = new SignupDetails(_emailSender);
            var data = signupDetails.ResetPassword(Username);
            if (data != "sent")
            {
                TempData["Error"] = data;
            }
            else
            {
                TempData["success"] = "Please check your mail for further instructions.";
            }
            return View();
        }

        [HttpGet]
        public IActionResult ValidatePasswordLink([FromQuery] string txn, [FromQuery] string user)
        {
            SignupDetails signupDetails = new SignupDetails();
            if (!signupDetails.ValidateResetLink(txn))
            {
                TempData["Error"] = "Password reset link has been expired or invalid";
                return RedirectToAction("ResetPassword");
            }
            else
            {
                TempData["txn"] = txn;
                TempData["username"] = user;
                return View("SetNewPassword");
            }
            
        }

        [HttpGet]
        public IActionResult SetNewPassword()
        {
            if (TempData["txn"] == null && TempData["username"] == null)
            {
                TempData["Error"] = "Invalid reset link. Please fallow the reset password again.";
                return View("ResetPassword");
            }
            else
            {
                SignupDetails signupDetails = new SignupDetails();
                if (!signupDetails.ValidateResetLink(TempData["txn"].ToString()))
                {
                    TempData["Error"] = "Password reset link has been expired or invalid";
                    return View("ResetPassword");
                }
                else
                {
                    TempData["username"] = TempData["username"].ToString();
                    return View();
                }
                
            }
        }

        [HttpPost]
        public IActionResult SetNewPassword([FromForm] SetPasswordModel setPasswordModel)
        {
            SignupDetails signupDetails = new SignupDetails();
            var data=signupDetails.SetNewPassword(setPasswordModel);
            if(data=="set")
            {
                TempData["success"] = "You have set your new password successfully.";
            }
            else
            {
                TempData["Error"] = data;
                TempData["username"] = setPasswordModel.Username;
            }
            return View();
        }

        [HttpGet]
        public IActionResult RemoveResetPasswordLink([FromQuery] string txn)
        {
            SignupDetails signupDetails = new SignupDetails();
            if (signupDetails.RemoveResetLink(txn))
            {
                return View();
            }
            else
            {
                TempData["Error"] = "Password reset link has been expired or invalid";
                return View("ResetPassword");
            }
                
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index","home");
        }

        [HttpGet]
        public IActionResult Signup()
        {
            DropdownData dropdownData = new DropdownData();
            return View(dropdownData.Get("usertype"));
        }
        [HttpPost]
        public IActionResult SaveSignup(SignupModel signupModel)
        {
            if (ModelState.IsValid)
            {
                SignupDetails signupDetails = new SignupDetails();
                var userdata = signupDetails.SaveSignUp(signupModel);
                if (userdata.UserId > 0)
                {
                    signupModel.UserId = userdata.UserId;
                    HttpContext.Session.SetObjectAsJson("signupModel",signupModel);
                    return View("CompleteProfile", signupModel);
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

        //[HttpGet]
        [LetsPaintAuth]
        public IActionResult CompleteProfile()
        {
            var signupData = HttpContext.Session.GetObjectFromJson<SignupModel>("signupModel");
            if (signupData != null)
                return View(signupData);
            else
            {
                DropdownData dropdownData = new DropdownData();
                return View("Signup",dropdownData.Get("usertype"));
            }
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

        [HttpPost]
        public string VerifyMobile([FromBody] VerifyMobileModel verifyMobileModel)
        {
            SignupDetails signupDetails = new SignupDetails();
            return signupDetails.VerifyMobile(verifyMobileModel);
        }
        [HttpPost]
        public string VerifyEmail([FromBody] VerifyEmailModel verifyEmailModel)
        {
            SignupDetails signupDetails = new SignupDetails();
            return signupDetails.VerifyEmail(verifyEmailModel);
        }

        [HttpPost]
        public bool Isemailexist([FromQuery] string email)
        {
            SignupDetails signupDetails = new SignupDetails();
            return signupDetails.IsEmailExist(email);
        }

        [HttpPost]
        public ApiResponseModel ChangePassword([FromForm] ChangePasswordModel changePasswordModel)
        {
            SignupDetails signupDetails = new SignupDetails();
            ApiResponseModel apiResponseModel = new ApiResponseModel();
            var loginData = HttpContext.Session.GetObjectFromJson<LoginOutputModel>("loginData");
            apiResponseModel.Message = signupDetails.ChangePassword(changePasswordModel, loginData);
            return apiResponseModel;
        }
    }
}