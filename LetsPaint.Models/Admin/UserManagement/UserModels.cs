using LetsPaint.ModelAccess.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsPaint.ModelAccess.Admin.UserManagement
{
    public class UserListModel
    {
        public bool IsBlocked { get; set; }
        public string UserType { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            { return this.FirstName + " " + this.LastName; }
            private set { }
        }

        public bool IsAdmin { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
    }

    public class UserBlockModel
    {
        public int UserId { get; set; }
        public bool IsBlocked { get; set; }
    }
}
