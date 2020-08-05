using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LetsPaint.DataAccess;
using LetsPaint.DataAccess.Models;
using LetsPaint.ModelAccess.Common;

namespace LetsPaint.BusinessAccess.Common
{
  public  class DropdownData
    {
        private readonly LetsPaintContext _db;
        public DropdownData()
        {
            _db = new LetsPaintContext();
        }

        public List<SelectListModel> Get(string key)
        {
            if(key.ToLowerInvariant() == "usertype")
            {
                return _db.MstUserType.Where(x => x.IsActive).Select(x => new SelectListModel() { Text = x.UserType, Value = x.UserTypeId }).ToList();
            }
            else if (key.ToLowerInvariant() == "artist")
            {
                var userType = _db.MstUserType.Where(x => x.IsActive && x.UserType.ToLowerInvariant() == key.ToLowerInvariant()).FirstOrDefault();
                if(userType!=null)
                {
                    return _db.MstUsers.Where(x => x.IsActive && x.UserTypeId.Equals(userType.UserTypeId)).Select(x => new SelectListModel() { Text = $"{x.FirstName} {x.LastName} ({x.Email})", Value = x.UserId }).ToList();
                }
            }
                return new List<SelectListModel>();
        }
    }
}
