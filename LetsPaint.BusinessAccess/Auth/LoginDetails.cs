using LetsPaint.DataAccess.Models;
using LetsPaint.ModelAccess.Auth;
using LetsPaint.ModelAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LetsPaint.BusinessAccess.Auth
{
    public class LoginDetails
    {

        private readonly LetsPaintContext _db;
        public LoginDetails()
        {
            _db = new LetsPaintContext();
        }

        public ApiResponseModel Login(LoginViewModel loginViewModel)
        {
            ApiResponseModel apiResponseModel = new ApiResponseModel();
            if (!string.IsNullOrEmpty(loginViewModel.Email) && !string.IsNullOrEmpty(loginViewModel.Email))
            {
               var _user= _db.MstUsers.Where(x => x.Email.Equals(loginViewModel.Email)).FirstOrDefault();
                if(_user!=null)
                {
                    if(_user.Password.Equals(loginViewModel.Password))
                    {
                        if (!_user.IsBlocked)
                        {var _data= new LoginOutputModel {IsAdmin=_user.IsAdmin, IsBlocked = _user.IsBlocked, FirstName = _user.FirstName, LastName = _user.FirstName, Email = _user.Email, Mobile = _user.Mobile, IsEmailVarified = _user.IsEmailVarified, IsMobileVarified = _user.IsMobileVerified, UserId = _user.UserId};
                            var _userDetails = _db.MstUserDetails.Where(x => x.UserId == _user.UserId && x.IsActive).FirstOrDefault();
                            if(_userDetails!=null)
                            {
                                _data.Photo = _userDetails.Photo;
                            }
                            apiResponseModel.Data = _data;
                            apiResponseModel.Message = "true";
                        }
                        else
                        {
                            apiResponseModel.Message = "Your account is blocked";
                        }
                    }
                    else
                    {
                        apiResponseModel.Message = "Username/Password is invalid";
                    }
                }
                else
                {
                    apiResponseModel.Message = "Username/Password is invalid";
                }
            }
                return apiResponseModel;
        }

    }
}
