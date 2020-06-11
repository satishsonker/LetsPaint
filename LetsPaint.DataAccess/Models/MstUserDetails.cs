using System;
using System.Collections.Generic;

namespace LetsPaint.DataAccess.Models
{
    public partial class MstUserDetails
    {
        public int UserDetailsId { get; set; }
        public int UserId { get; set; }
        public string FacebookProfile { get; set; }
        public string InstagramProfile { get; set; }
        public string Website { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Photo { get; set; }

        public virtual MstUsers User { get; set; }
    }
}
