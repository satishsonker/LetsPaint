using System;
using System.Collections.Generic;

namespace LetsPaint.DataAccess.Models
{
    public partial class MstRefLookup
    {
        public int RefId { get; set; }
        public string RefKey { get; set; }
        public string RefValue { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual MstUsers CreatedByNavigation { get; set; }
    }
}
