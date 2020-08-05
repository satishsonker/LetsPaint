using LetsPaint.DataAccess.Models;
using LetsPaint.ModelAccess.Admin.GalleryManagement;
using LetsPaint.ModelAccess.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LetsPaint.BusinessAccess.Admin.GalleryManagement
{
   public class GalleryManagementDetails
    {
        private readonly LetsPaintContext _db;
        public GalleryManagementDetails()
        {
            _db = new LetsPaintContext();
        }
        public PagingOutModel GetGalleryType(PagingInModel pagingInModel)
        {
            PagingOutModel pagingModel = new PagingOutModel
            {
                PageNo = pagingInModel.PageNo,
                PageSize = pagingInModel.PageSize
            };
            var data = _db.MstGalleryType.Where(x => x.IsActive && (pagingInModel.Id==0 || x.GalleryTypeId==pagingInModel.Id)).Select(x => new GalleryTypeModel()
            {
               GalleryType=x.GalleryType,
               GalleryTypeId=x.GalleryTypeId,
               BaseUrl=x.BaseUrl,
               GridSize=x.GridSize
            }).ToList();
            pagingModel.Data = data.Skip((pagingInModel.PageNo - 1) * pagingInModel.PageSize).Take(pagingInModel.PageSize).ToList();
            pagingModel.TotalRecord = data.Count;
            return pagingModel;
        } 
        public ApiResponseModel SaveGalleryType(GalleryTypeModel galleryTypeModel)
        {
            ApiResponseModel apiResponseModel = new ApiResponseModel();
            apiResponseModel.Message = "Saved";
            if (galleryTypeModel != null)
            {
                MstGalleryType mstGalleryType = new MstGalleryType()
                {
                    BaseUrl = galleryTypeModel.BaseUrl,
                    CreatedDate = DateTime.Now,
                    GalleryType = galleryTypeModel.GalleryType,
                    GridSize = galleryTypeModel.GridSize,
                    IsActive = true,
                    CreatedBy = galleryTypeModel.UserId
                };
                _db.MstGalleryType.Add(mstGalleryType);
                if (_db.SaveChanges() < 1)
                {
                    apiResponseModel.Message = "Unable to save.";
                }
            }
            return apiResponseModel;
        }
        public ApiResponseModel UpdateGalleryType(GalleryTypeModel galleryTypeModel)
        {
            ApiResponseModel apiResponseModel = new ApiResponseModel
            {
                Message = "Updated",
                MessageType = ApiResponseMessageType.success
            };
            if (galleryTypeModel != null)
            {
                var _oldData = _db.MstGalleryType.Where(x => x.GalleryTypeId.Equals(galleryTypeModel.GalleryTypeId) && x.IsActive).FirstOrDefault();
                if(_oldData!=null)
                {
                    _oldData.BaseUrl = galleryTypeModel.BaseUrl;
                    _oldData.GridSize = galleryTypeModel.GridSize;
                    _oldData.GalleryType = galleryTypeModel.GalleryType;
                    _oldData.ModifiedDate = DateTime.Now;
                    _oldData.ModifiedBy = galleryTypeModel.UserId;
                    _db.Entry(_oldData).State = EntityState.Modified;
                    if (_db.SaveChanges() < 1)
                    {
                        apiResponseModel.Message = "Unable to update.";
                        apiResponseModel.MessageType = ApiResponseMessageType.error;
                    }
                }
            }
            else
            {
                apiResponseModel.Message = "Unable to find record";
                apiResponseModel.MessageType = ApiResponseMessageType.warning;
            }
            return apiResponseModel;
        }
        public ApiResponseModel DeleteGalleryType(GalleryTypeModel galleryTypeModel)
        {
            ApiResponseModel apiResponseModel = new ApiResponseModel
            {
                Message = "Deleted",
                MessageType = ApiResponseMessageType.success
            };
            if (galleryTypeModel != null)
            {
                var _oldData = _db.MstGalleryType.Where(x => x.GalleryTypeId.Equals(galleryTypeModel.GalleryTypeId) && x.IsActive).FirstOrDefault();
                if (_oldData != null)
                {
                    _oldData.IsActive = false;
                    _oldData.ModifiedDate = DateTime.Now;
                    _oldData.ModifiedBy = galleryTypeModel.UserId;

                    _db.Entry(_oldData).State = EntityState.Modified;
                    if (_db.SaveChanges() < 1)
                    {
                        apiResponseModel.Message = "Unable to delete.";
                        apiResponseModel.MessageType = ApiResponseMessageType.error;
                    }
                }
            }
            else
            {
                apiResponseModel.Message = "Unable to find record";
                apiResponseModel.MessageType = ApiResponseMessageType.warning;
            }
            return apiResponseModel;
        }

        public PagingOutModel GetGallery(PagingInModel pagingInModel)
        {
            PagingOutModel pagingModel = new PagingOutModel
            {
                PageNo = pagingInModel.PageNo,
                PageSize = pagingInModel.PageSize
            };
            var data = _db.MstGallery.Where(x => x.IsActive == true && (pagingInModel.Id == 0 || x.GalleryId == pagingInModel.Id)).Select(x => new GalleryModel()
            {
                ArtistId = x.ArtistId,
                ArtistName = x.Artist.FirstName + "" + x.Artist.LastName,
                AvailableQy = x.AvailableQy,
                Badge = x.Badge,
                Description = x.Description,
                GalleryId = x.GalleryId,
                GalleryTypeId = x.GalleryTypeId,
                HasArtistCertificate = Convert.ToBoolean(x.HasArtistCertificate),
                HasArtistSign = Convert.ToBoolean(x.HasArtistSign),
                Image = x.Image,
                IsAvailable = Convert.ToBoolean(x.IsAvailable),
                Medium = x.Medium,
                Price = x.Price,
                Size = x.Size,
                SuitableFor = x.SuitableFor,
                Surface = x.Surface,
                Tags = x.Tags,
                Thumbnail = x.Thumbnail,
                Title = x.Title, 
                BaseUrl = x.GalleryType.BaseUrl
            }).ToList();
            pagingModel.Data = data.Skip((pagingInModel.PageNo - 1) * pagingInModel.PageSize).Take(pagingInModel.PageSize).ToList();
            pagingModel.TotalRecord = data.Count;
            return pagingModel;
        }
        public ApiResponseModel SaveGallery(GalleryModel galleryModel)
        {
            ApiResponseModel apiResponseModel = new ApiResponseModel();
            apiResponseModel.Message = "Saved";
            if (galleryModel != null)
            {
                MstGallery mstGallery = new MstGallery()
                {
                    ArtistId = galleryModel.ArtistId,
                    CreatedDate = DateTime.Now,
                    GalleryTypeId = galleryModel.GalleryTypeId,
                    HasArtistCertificate = galleryModel.HasArtistCertificate,
                    AvailableQy = galleryModel.AvailableQy,
                    Badge = galleryModel.Badge,
                    CreatedBy = galleryModel.UserId,
                    Description = galleryModel.Description,
                    HasArtistSign = galleryModel.HasArtistSign,
                    Image = galleryModel.Image,
                    IsActive = true,
                    IsAvailable = galleryModel.IsAvailable,
                    Medium= galleryModel.Medium,
                    Title= galleryModel.Title,
                    Price= galleryModel.Price,
                    Size= galleryModel.Size,
                    Thumbnail= galleryModel.Thumbnail,
                    Tags= galleryModel.Tags,
                    Surface= galleryModel.Surface,
                    SuitableFor= galleryModel.SuitableFor                    
                };
                _db.MstGallery.Add(mstGallery);
                if (_db.SaveChanges() < 1)
                {
                    apiResponseModel.Message = "Unable to save.";
                }
            }
            return apiResponseModel;
        }
        public ApiResponseModel UpdateGallery(GalleryModel galleryModel)
        {
            ApiResponseModel apiResponseModel = new ApiResponseModel
            {
                Message = "Updated",
                MessageType = ApiResponseMessageType.success
            };
            if (galleryModel != null)
            {
                var _oldData = _db.MstGallery.Where(x => x.GalleryId.Equals(galleryModel.GalleryId) && x.IsActive==true).FirstOrDefault();
                if (_oldData != null)
                {
                     _oldData.ArtistId = galleryModel.ArtistId;
                     _oldData.GalleryTypeId = galleryModel.GalleryTypeId;
                     _oldData.HasArtistCertificate = galleryModel.HasArtistCertificate;
                     _oldData.AvailableQy = galleryModel.AvailableQy;
                     _oldData.Badge = galleryModel.Badge;
                     _oldData.Description = galleryModel.Description;
                     _oldData.HasArtistSign = galleryModel.HasArtistSign;
                     _oldData.Image = galleryModel.Image;
                     _oldData.IsActive = true;
                     _oldData.IsAvailable = galleryModel.IsAvailable;
                     _oldData.Medium = galleryModel.Medium;
                     _oldData.Title = galleryModel.Title;
                     _oldData.Price = galleryModel.Price;
                     _oldData.Size = galleryModel.Size;
                     _oldData.Thumbnail = galleryModel.Thumbnail;
                     _oldData.Tags = galleryModel.Tags;
                     _oldData.Surface = galleryModel.Surface;
                     _oldData.SuitableFor = galleryModel.SuitableFor;
                    _oldData.ModifiedDate = DateTime.Now;
                    _oldData.Modifiedby = galleryModel.UserId;
                    _db.Entry(_oldData).State = EntityState.Modified;
                    if (_db.SaveChanges() < 1)
                    {
                        apiResponseModel.Message = "Unable to update.";
                        apiResponseModel.MessageType = ApiResponseMessageType.error;
                    }
                }
            }
            else
            {
                apiResponseModel.Message = "Unable to find record";
                apiResponseModel.MessageType = ApiResponseMessageType.warning;
            }
            return apiResponseModel;
        }
        public ApiResponseModel DeleteGallery(GalleryModel galleryModel)
        {
            ApiResponseModel apiResponseModel = new ApiResponseModel
            {
                Message = "Deleted",
                MessageType = ApiResponseMessageType.success
            };
            if (galleryModel != null)
            {
                var _oldData = _db.MstGallery.Where(x => x.GalleryId.Equals(galleryModel.GalleryId) && x.IsActive==true).FirstOrDefault();
                if (_oldData != null)
                {
                    _oldData.IsActive = false;
                    _oldData.ModifiedDate = DateTime.Now;
                    _oldData.Modifiedby = galleryModel.UserId;

                    _db.Entry(_oldData).State = EntityState.Modified;
                    if (_db.SaveChanges() < 1)
                    {
                        apiResponseModel.Message = "Unable to delete.";
                        apiResponseModel.MessageType = ApiResponseMessageType.error;
                    }
                }
            }
            else
            {
                apiResponseModel.Message = "Unable to find record";
                apiResponseModel.MessageType = ApiResponseMessageType.warning;
            }
            return apiResponseModel;
        }
    }
}
