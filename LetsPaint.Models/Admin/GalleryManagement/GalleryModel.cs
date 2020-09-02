using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace LetsPaint.ModelAccess.Admin.GalleryManagement
{
    public class GalleryTypeModel
    {
        public int GalleryTypeId { get; set; }
        public string GalleryType { get; set; }
        public int GridSize { get; set; }
        public string BaseUrl { get; set; }
        public int UserId { get; set; }
    }

    public class GalleryModel
    {
        public int GalleryTypeId { get; set; }
        public int GridSize { get; set; }
        public int GalleryId { get; set; }
        public string BaseUrl { get; set; }
        public string GalleryType { get; set; }
        public int UserId { get; set; }
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public int AvailableQy { get; set; }
        public string Badge { get; set; }
        public string Description { get; set; }
        public bool HasArtistCertificate { get; set; }
        public bool HasArtistSign { get; set; }
        public string Image { get; set; }
        public bool IsAvailable { get; set; }
        public string Medium { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
        public string SuitableFor { get; set; }
        public string Surface { get; set; }
        public string Tags { get; set; }
        public string Thumbnail { get; set; }
        public string Title { get; set; }
        public IFormFile file { get; set; }
    }

    public class GalleryImageDeleteModel
    {
        public int GalleryId { get; set; }
        public int UserId { get; set; }
        public string BaseUrl { get; set; }
    }
}
