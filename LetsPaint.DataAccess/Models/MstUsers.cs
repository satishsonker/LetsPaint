using System;
using System.Collections.Generic;

namespace LetsPaint.DataAccess.Models
{
    public partial class MstUsers
    {
        public MstUsers()
        {
            MstGalleryArtist = new HashSet<MstGallery>();
            MstGalleryCreatedByNavigation = new HashSet<MstGallery>();
            MstRefLookup = new HashSet<MstRefLookup>();
            MstUserDetails = new HashSet<MstUserDetails>();
        }

        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsActive { get; set; }
        public bool IsBlocked { get; set; }
        public bool? IsDefaultPassword { get; set; }
        public bool IsEmailVarified { get; set; }
        public bool IsMobileVerified { get; set; }
        public bool? IsPasswordReset { get; set; }
        public string PasswordResetCode { get; set; }
        public string Password { get; set; }
        public byte LoginAttempt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual MstUserType UserType { get; set; }
        public virtual ICollection<MstGallery> MstGalleryArtist { get; set; }
        public virtual ICollection<MstGallery> MstGalleryCreatedByNavigation { get; set; }
        public virtual ICollection<MstRefLookup> MstRefLookup { get; set; }
        public virtual ICollection<MstUserDetails> MstUserDetails { get; set; }
    }
}
