using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LetsPaint.DataAccess;
using LetsPaint.DataAccess.Models;
using LetsPaint.ModelAccess.Common;
using LetsPaint.ModelAccess.Gallery;

namespace LetsPaint.BusinessAccess.Gallery
{
    public class GalleryDetails
    {
        private readonly LetsPaintContext _db;
        public GalleryDetails()
        {
            _db = new LetsPaintContext();
        }

        public ApiResponseModel GetGalleryType()
        {
            var data= _db.MstGalleryType.Where(x => x.IsActive).Select(x =>
            new GalleryTypeModel()
            {
                BaseUrl = x.BaseUrl,
                GalleryType = x.GalleryType,
                GalleryTypeId = x.GalleryTypeId,
                GridSize = x.GridSize
            }).ToList();
            return new ApiResponseModel() { Data = data };
        }

        public object GetGallery(int GalleryTypeId, int PageNo = 1, int PageSize = 10)
        {
            var data = _db.MstGallery
                .Where(x => Convert.ToBoolean(x.IsActive) && x.GalleyTypeId == GalleryTypeId).OrderByDescending(x => x.CreatedDate)
                .Select(x => new GalleryModel()
                {
                    ArtistId = x.ArtistId,
                    ArtistName = x.Artist.FirstName + "" + x.Artist.LastName,
                    AvailableQy = x.AvailableQy,
                    Badge = x.Badge,
                    Description = x.Description,
                    GalleryId = x.GalleryId,
                    GalleyTypeId = x.GalleyTypeId,
                    HasArtistCertificate = x.HasArtistCertificate,
                    HasArtistSign = x.HasArtistSign,
                    Image = x.Image,
                    IsAvailable = x.IsAvailable,
                    Medium = x.Medium,
                    Price = x.Price,
                    Size = x.Size,
                    SuitableFor = x.SuitableFor,
                    Surface = x.Surface,
                    Tags = x.Tags,
                    Thumbnail = x.Thumbnail,
                    Title = x.Title
                })
                .ToList();
            return new ApiResponseModel() { 
            Data=new { TotalRecord=data.Count,Records= data.Skip(PageSize * (PageNo - 1)).Take(PageSize).ToList() }
            }; 
        }
    }
}
