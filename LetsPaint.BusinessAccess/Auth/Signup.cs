using LetsPaint.DataAccess.Models;
using LetsPaint.ModelAccess.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsPaint.BusinessAccess.Auth
{
    public class SignupDetails
    {
        private readonly LetsPaintContext _db;
        public SignupDetails()
        {
            _db = new LetsPaintContext();
        }

        public bool SaveSignUp(SignupModel signupModel)
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
           return _db.SaveChanges()>0?true:false;
        }

    }
}
