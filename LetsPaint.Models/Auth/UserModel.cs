using System;
using System.Collections.Generic;
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

    }
}
