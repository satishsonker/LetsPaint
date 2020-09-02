using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LetsPaint.DataAccess;
using LetsPaint.DataAccess.Models;
using LetsPaint.ModelAccess.Common;
using LetsPaint.ModelAccess.Gallery;
using Microsoft.EntityFrameworkCore;

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

        public object GetGallery(int GalleryTypeId, int PageNo = 1, int PageSize = 30)
        {
            var data = (from gal in _db.MstGallery
                       from user in _db.MstUsers.DefaultIfEmpty()
                where Convert.ToBoolean(gal.IsActive) && gal.GalleryTypeId == GalleryTypeId && gal.ArtistId==user.UserId
                orderby gal.CreatedDate descending
                       select new GalleryModel()
                       {
                           ArtistId = gal.ArtistId,
                           ArtistName = user.FirstName+" "+user.LastName,
                           AvailableQy = gal.AvailableQy,
                           Badge = gal.Badge,
                           Description = gal.Description,
                           GalleryId = gal.GalleryId,
                           GalleyTypeId = gal.GalleryTypeId,
                           HasArtistCertificate = gal.HasArtistCertificate,
                           HasArtistSign = gal.HasArtistSign,
                           Image = gal.Image,
                           IsAvailable = gal.IsAvailable,
                           Medium = gal.Medium,
                           Price = gal.Price,
                           Size = gal.Size,
                           SuitableFor = gal.SuitableFor,
                           Surface = gal.Surface,
                           Tags = gal.Tags,
                           Thumbnail = gal.Thumbnail,
                           Title = gal.Title
                       })
                .ToList();
            return new ApiResponseModel() { 
            Data=new { TotalRecord=data.Count,Records= data.Skip(PageSize * (PageNo - 1)).Take(PageSize).ToList() }
            }; 
        }
    }
}
