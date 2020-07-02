using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LetsPaint.ModelAccess.Auth
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
    }

    public class UserProfileModel: UserModel
    {
        public int UserDetailsId { get; set; }
        public string FacebookProfile { get; set; }
        public string InstagramProfile { get; set; }
        public string Website { get; set; }
        public string Photo { get; set; }
        public string Gender { get; set; }
    }

    public class VerifyEmailModel
    {
        public string Email { get; set; }
        [Range(100000,999999,ErrorMessage ="Invalid OTP")]
        public int OTP { get; set; }
    }
    public class VerifyMobileModel
    {
        [StringLength(13,ErrorMessage ="Invalid Mobile Number",MinimumLength =10)]
        public string Mobile { get; set; }
        [Range(100000, 999999, ErrorMessage = "Invalid OTP")]
        public int OTP { get; set; }
    }
    public class ChangePasswordModel 
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
