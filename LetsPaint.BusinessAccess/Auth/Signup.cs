using LetsPaint.DataAccess.Models;
using LetsPaint.ModelAccess.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using LetsPaint.ModelAccess;

namespace LetsPaint.BusinessAccess.Auth
{
    public class SignupDetails
    {
        private readonly LetsPaintContext _db;
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
            return _db.SaveChanges() > 0 ? true : false;
        }

    }
}
