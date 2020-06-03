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
            if(key.ToLower()=="usertype")
           return _db.MstUserType.Where(x => x.IsActive).Select(x => new SelectListModel() { Text = x.UserType, Value = x.UserTypeId }).ToList();
            else
            {
                return new List<SelectListModel>();
            }
        }
    }
}
