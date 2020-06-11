using System;
using System.Collections.Generic;

namespace LetsPaint.DataAccess.Models
{
    public partial class MstGalleryType
    {
        public MstGalleryType()
        {
            MstGallery = new HashSet<MstGallery>();
        }

        public int GalleryTypeId { get; set; }
        public string GalleryType { get; set; }
        public string BaseUrl { get; set; }
        public int GridSize { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<MstGallery> MstGallery { get; set; }
    }
}
