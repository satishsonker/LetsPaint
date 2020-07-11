using LetsPaint.DataAccess.Models;
using LetsPaint.ModelAccess.Admin.UserManagement;
using LetsPaint.ModelAccess.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LetsPaint.BusinessAccess.Admin.UserManagement
{
    public class UserManagementDetails
    {
        private readonly LetsPaintContext _db;
        public UserManagementDetails()
        {
            _db = new LetsPaintContext();
        }
        public PagingOutModel GetUserList(PagingInModel pagingInModel)
        {
            PagingOutModel pagingModel = new PagingOutModel
            {
                PageNo = pagingInModel.PageNo,
                PageSize = pagingInModel.PageSize
            };
            var data = _db.MstUsers.Where(x => x.IsActive).Select(x => new UserListModel()
            {
                Email = x.Email,
                FirstName=x.FirstName,
                IsAdmin=x.IsAdmin,
               LastName=x.LastName,
               Gender=x.Gender,
               Mobile=x.Mobile,
               IsBlocked=x.IsBlocked,
               UserType=x.UserType.UserType,
               Photo=x.MstUserDetails.Where(y=>y.IsActive).FirstOrDefault().Photo,UserId=x.UserId
            }).ToList();
            pagingModel.Data = data.Skip((pagingInModel.PageNo - 1) * pagingInModel.PageSize).Take(pagingInModel.PageSize).ToList();
            pagingModel.TotalRecord = data.Count;
            return pagingModel;
        }
        public ApiResponseModel BlockUser(UserBlockModel userBlockModel)
        {
            ApiResponseModel apiResponseModel = new ApiResponseModel();
            var user = _db.MstUsers.Where(x => x.IsActive && x.UserId.Equals(userBlockModel.UserId)).FirstOrDefault();
            if(user!=null)
            {
                user.IsBlocked = userBlockModel.IsBlocked;
                user.ModifiedDate = DateTime.Now;
                _db.Entry(user).State = EntityState.Modified;
                if(_db.SaveChanges()>0)
                {
                    apiResponseModel.Message =userBlockModel.IsBlocked ? "Blocked" : "Unblocked";
                }
                else
                {
                    apiResponseModel.Message = "Unable to block user.";
                }
            }
            else
            {
                apiResponseModel.Message = "User not found";
            }
            return apiResponseModel;
        }
        public ApiResponseModel DeleteUser(int userId)
        {
            ApiResponseModel apiResponseModel = new ApiResponseModel();
            var user = _db.MstUsers.Where(x => x.IsActive && x.UserId.Equals(userId)).FirstOrDefault();
            if (user != null)
            {
                user.IsActive = false;
                user.ModifiedDate = DateTime.Now;
                _db.Entry(user).State = EntityState.Modified;
                    apiResponseModel.Message = _db.SaveChanges() > 0 ? "Deleted" : "NotDeleted";
            }
            else
            {
                apiResponseModel.Message = "User not found";
            }
            return apiResponseModel;
        }
    }
}
