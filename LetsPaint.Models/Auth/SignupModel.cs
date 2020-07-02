using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LetsPaint.ModelAccess.Auth
{
    public class SignupModel
    {
        [Required]
        [StringLength(50,MinimumLength =3,ErrorMessage ="Min 3 char in Name")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage ="Invalid Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Min 6 char in Password")]
        public string Password { get; set; }
        [Required]
        public int UserType { get; set; }
        public int UserId { get; set; }
    }
    public class SetPasswordModel
    {
        public string Username { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string txn { get; set; }
    }
}
