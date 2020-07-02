using LetsPaint.DataAccess.Models;
using LetsPaint.ModelAccess.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using LetsPaint.ModelAccess;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LetsPaint.UtilityManager;
using LetsPaint.ModelAccess.Models;
using System.IO;

namespace LetsPaint.BusinessAccess.Auth
{
    public class SignupDetails
    {
        private readonly LetsPaintContext _db;
        private readonly IEmailSender _emailSender;
        public SignupDetails(IEmailSender emailSender)
        {
            _db = new LetsPaintContext();
            _emailSender = emailSender;
        }
        public SignupDetails()
        {
            _db = new LetsPaintContext();
        }

        public MstUsers SaveSignUp(SignupModel signupModel)
        {
            MstUsers mstUsers = new MstUsers()
            {
                CreatedBy = 1,
                CreatedDate = DateTime.Now,
                Email = signupModel.Email,
                FirstName = signupModel.Name,
                IsActive = true,
                IsBlocked = false,
                IsDefaultPassword = false,
                IsEmailVarified = false,
                IsMobileVerified = false,
                IsPasswordReset = false,
                LoginAttempt = 0,
                Password = signupModel.Password,
                UserTypeId = signupModel.UserType
            };
            _db.MstUsers.Add(mstUsers);
            _db.SaveChanges();
           return mstUsers;
        }


        public bool SaveProfile(UserProfileModel userProfileModel)
        {
            var _user = _db.MstUsers.Where(x => x.UserId == userProfileModel.UserId && x.IsActive).FirstOrDefault();
            if (_user != null)
            {
                _user.Gender = userProfileModel.Gender;
                _user.ModifiedBy = userProfileModel.UserId;
                _user.ModifiedDate = DateTime.Now;
                _db.Entry(_user).State = EntityState.Modified;
                MstUserDetails mstUserDetails = new MstUserDetails()
                {
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now,
                    FacebookProfile = userProfileModel.FacebookProfile,
                    InstagramProfile = userProfileModel.InstagramProfile,
                    IsActive = true,
                    UserId = userProfileModel.UserId,
                    Website = userProfileModel.Website,
                    Photo = userProfileModel.Photo
                };
                _db.MstUserDetails.Add(mstUserDetails);
            }
            return _db.SaveChanges() > 0 ? true : false;
        }

        public string VerifyEmail(VerifyEmailModel verifyEmailModel)
        {
            var user = _db.MstUsers.Where(x =>x.IsActive && x.Email.Equals(verifyEmailModel.Email) &&x.EmailOtp==verifyEmailModel.OTP).FirstOrDefault();
            if(user==null)
            {
                return "Invalid OTP";
            }
            else
            {
                user.IsEmailVarified = true;
                user.ModifiedBy = user.UserId;
                user.ModifiedDate = DateTime.Now;
                _db.Entry(user).State = EntityState.Modified;
                return _db.SaveChanges() > 0 ? "verified" : "notverified";
            }
            
        }
        public string VerifyMobile(VerifyMobileModel verifyMobileModel)
        {
            var user = _db.MstUsers.Where(x => x.IsActive && x.Email.Equals(verifyMobileModel.Mobile) && x.EmailOtp == verifyMobileModel.OTP).FirstOrDefault();
            if (user == null)
            {
                return "Invalid OTP";
            }
            else
            {
                user.IsEmailVarified = true;
                user.ModifiedBy = user.UserId;
                user.ModifiedDate = DateTime.Now;
                _db.Entry(user).State = EntityState.Modified;
                return _db.SaveChanges() > 0 ? "verified" : "notverified";
            }

        }

        public bool IsEmailExist(string email)
        {
            var user = _db.MstUsers.Where(x => x.IsActive && x.Email.Equals(email)).FirstOrDefault();
            return user == null ? false : true;
        }

        public string ChangePassword(ChangePasswordModel changePasswordModel, LoginOutputModel loginOutputModel)
        {
            string msg = string.Empty; ;
            if (loginOutputModel != null && !string.IsNullOrEmpty(loginOutputModel.Email))
            {
               var userData= _db.MstUsers.Where(x => x.IsActive && x.Email.Equals(loginOutputModel.Email) && x.Password.Equals(changePasswordModel.OldPassword)).FirstOrDefault();
                if(userData!=null)
                {
                    if (changePasswordModel.NewPassword==changePasswordModel.ConfirmPassword)
                    {
                        userData.Password = changePasswordModel.ConfirmPassword;
                        userData.ModifiedBy = loginOutputModel.UserId;
                        userData.ModifiedDate = DateTime.Now;
                        _db.Entry(userData).State = EntityState.Modified;
                        _db.SaveChanges();
                        msg = "changed";
                    }
                    else
                    {
                        msg = "New password and confirm password are not same.";
                    }
                }
                else
                {
                    msg = "Invalid Old password.";
                }
            }
            else { 
                msg="You are not logged in."; 
            }
            return msg;
        }

        public string ResetPassword(string username)
        {
          var hostname=  AppSettingManager.GetAppSettingsData(ConstantManager.AppSetting.LpVariables, ConstantManager.AppSetting.Hostname,string.Empty);
            Guid guid = Guid.NewGuid();
            var user= _db.MstUsers.Where(x => x.Email.Equals(username) && x.IsActive).FirstOrDefault();
            string msg = string.Empty;
            string link = $"{hostname}/auth/ValidatePasswordLink?txn={guid.ToString()}&user={username}";
            string removeLink = $"{hostname}/auth/removeResetPasswordLink?txn={guid.ToString()}"; ;
            if (user!=null)
            {
                user.IsPasswordReset = true;
                user.PasswordResetCode = guid.ToString();
                user.ModifiedDate = DateTime.Now;
                _db.Entry(user).State = EntityState.Modified;
                if (_db.SaveChanges() > 0)
                {
                    msg = "sent";
                    var temp = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "emailTemplate", "resetpassword.html"));
                    var message = new Message(new string[] { "btech.csit@gmail.com", username }, "Password Reset", temp.Replace("##removereset##",removeLink).Replace("##resetlink##", link));
                    _emailSender.SendEmail(message);
                }
                else
                {
                    msg = "notsent";
                }
            }
            else
            {
                msg = "Invalid username.";
            }
            return msg;
        }

        public bool ValidateResetLink(string txn)
        {
            int resetlinkexpire = AppSettingManager.GetAppSettingsData<int>(ConstantManager.AppSetting.LpVariables, ConstantManager.AppSetting.ResetLinkExpireInHour, "24");
            var user = _db.MstUsers.Where(x => x.PasswordResetCode.Equals(txn) && x.IsActive && x.IsPasswordReset == true && DateTime.Now<=x.ModifiedDate.Value.AddHours(resetlinkexpire)).FirstOrDefault();
            return user != null ? true : false;
        }

        public bool RemoveResetLink(string txn)
        {
            bool isRemoved = false;
            int resetlinkexpire = AppSettingManager.GetAppSettingsData<int>(ConstantManager.AppSetting.LpVariables, ConstantManager.AppSetting.ResetLinkExpireInHour, "24");

            var user = _db.MstUsers.Where(x => x.PasswordResetCode.Equals(txn) && x.IsActive && x.IsPasswordReset == true && DateTime.Now <= x.ModifiedDate.Value.AddHours(resetlinkexpire)).FirstOrDefault();
            if (user != null)
            {
                user.IsPasswordReset = false;
                user.PasswordResetCode = string.Empty;
                user.ModifiedDate = DateTime.Now;
                _db.Entry(user).State = EntityState.Modified;
                isRemoved = _db.SaveChanges() > 0 ? true : false;
            }
            return isRemoved;
        }

        public string SetNewPassword(SetPasswordModel setPasswordModel)
        {
            string msg = string.Empty;
            var user = _db.MstUsers.Where(x => x.IsActive && x.Email.Equals(setPasswordModel.Username)).FirstOrDefault();
            if(user==null)
            {
                msg = "User does not exists";
            }
            else
            {
                user.Password = setPasswordModel.ConfirmPassword;
                user.ModifiedBy = user.UserId;
                user.ModifiedDate = DateTime.Now;
                user.IsPasswordReset = false;
                user.PasswordResetCode = null;
                _db.Entry(user).State = EntityState.Modified;
                msg =_db.SaveChanges() > 0 ? "set" : "Unable to set new password";
            }
            return msg;
        }

    }
}
