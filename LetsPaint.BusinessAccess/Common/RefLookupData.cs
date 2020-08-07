using LetsPaint.DataAccess.Models;
using LetsPaint.ModelAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LetsPaint.BusinessAccess.Common
{
  public  class RefLookupData
    {
        private readonly LetsPaintContext _db;
        public RefLookupData()
        {
            _db = new LetsPaintContext();
        }

        public List<RefLookupModel> Get(List<string> key)
        {
            if (key.Count>0)
            {
                return _db.MstRefLookup.Where(x => x.IsActive && key.Any(y=>y==x.RefKey)).Select(x => new RefLookupModel() {RefId=x.RefId,RefKey=x.RefKey,RefValue=x.RefValue }).ToList();
            }
            return new List<RefLookupModel>();
        }
    }
}
