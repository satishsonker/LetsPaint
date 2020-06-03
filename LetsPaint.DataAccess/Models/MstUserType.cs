using System;
using System.Collections.Generic;

namespace LetsPaint.DataAccess.Models
{
    public partial class MstUserType
    {
        public MstUserType()
        {
            MstUsers = new HashSet<MstUsers>();
        }

        public int UserTypeId { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<MstUsers> MstUsers { get; set; }
    }
}
