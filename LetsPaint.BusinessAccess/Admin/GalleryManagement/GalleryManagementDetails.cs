using LetsPaint.DataAccess.Models;
using LetsPaint.ModelAccess.Admin.GalleryManagement;
using LetsPaint.ModelAccess.Common;
using LetsPaint.BusinessAccess.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace LetsPaint.BusinessAccess.Admin.GalleryManagement
{
    public class GalleryManagementDetails
    {
        private readonly LetsPaintContext _db;
        private readonly FileUploadDetails fileUploadDetails;
        public GalleryManagementDetails(IHostingEnvironment hostingEnvironment)
        {
            _db = new LetsPaintContext();
            fileUploadDetails = new FileUploadDetails(hostingEnvironment);
        }
        public PagingOutModel GetGalleryType(PagingInModel pagingInModel)
        {
            PagingOutModel pagingModel = new PagingOutModel
            {
                PageNo = pagingInModel?.PageNo ?? 0,
                PageSize = pagingInModel?.PageSize ?? 10
            };
            var data = _db.MstGalleryType.Where(x => x.IsActive && (pagingInModel == null || pagingInModel.Id == 0 || x.GalleryTypeId == pagingInModel.Id)).Select(x => new GalleryTypeModel()
            {
                GalleryType = x.GalleryType,
                GalleryTypeId = x.GalleryTypeId,
                BaseUrl = x.BaseUrl,
                GridSize = x.GridSize
            }).ToList();
            pagingModel.Data = pagingInModel == null ? data : data.Skip((pagingInModel.PageNo - 1) * pagingInModel.PageSize).Take(pagingInModel.PageSize).ToList();
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
                if (_oldData != null)
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
                GridSize=x.GalleryType.GridSize,
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

                string _image = string.Empty;
                string _thumbnail = string.Empty;
                if (galleryModel.file != null)
                {
                    _image = fileUploadDetails.UploadProductImage(galleryModel.file, "\\images\\product\\" + galleryModel.GalleryType.Trim().Replace(" ","") + "\\");
                    _thumbnail = fileUploadDetails.ResizeImage(_image, 150, 300);
                }
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
                    Image = Path.GetFileName(_image),
                    IsActive = true,
                    IsAvailable = galleryModel.IsAvailable,
                    Medium = galleryModel.Medium,
                    Title = galleryModel.Title,
                    Price = galleryModel.Price,
                    Size = galleryModel.Size,
                    Thumbnail = Path.GetFileName(_thumbnail),
                    Tags = galleryModel.Tags,
                    Surface = galleryModel.Surface,
                    SuitableFor = galleryModel.SuitableFor
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
                var _oldData = _db.MstGallery.Where(x => x.GalleryId.Equals(galleryModel.GalleryId) && x.IsActive == true).FirstOrDefault();
                if (_oldData != null)
                {
                    string _image = string.Empty;
                    string _thumbnail = string.Empty;
                    if (galleryModel.file != null)
                    {
                        _image = fileUploadDetails.UploadProductImage(galleryModel.file, "\\images\\Product\\" + galleryModel.GalleryType.Trim() + "\\");
                        _thumbnail = fileUploadDetails.ResizeImage(_image, 150, 300);
                    }
                    _oldData.ArtistId = galleryModel.ArtistId;
                    _oldData.GalleryTypeId = galleryModel.GalleryTypeId;
                    _oldData.HasArtistCertificate = galleryModel.HasArtistCertificate;
                    _oldData.AvailableQy = galleryModel.AvailableQy;
                    _oldData.Badge = galleryModel.Badge;
                    _oldData.Description = galleryModel.Description;
                    _oldData.HasArtistSign = galleryModel.HasArtistSign;
                    _oldData.IsActive = true;
                    _oldData.IsAvailable = galleryModel.IsAvailable;
                    _oldData.Medium = galleryModel.Medium;
                    _oldData.Title = galleryModel.Title;
                    _oldData.Price = galleryModel.Price;
                    _oldData.Size = galleryModel.Size;
                    if (!string.IsNullOrEmpty(_image))
                    {
                        _oldData.Image = _image.Substring(_image.LastIndexOf("\\") + 1);
                        _oldData.Thumbnail = _image.Substring(_thumbnail.LastIndexOf("\\") + 1);
                    }
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
                var _oldData = _db.MstGallery.Where(x => x.GalleryId.Equals(galleryModel.GalleryId) && x.IsActive == true).FirstOrDefault();
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
        public ApiResponseModel DeleteGalleryImage(GalleryImageDeleteModel galleryImageDeleteModel)
        {
            ApiResponseModel apiResponseModel = new ApiResponseModel
            {
                Message = "Deleted",
                MessageType = ApiResponseMessageType.success
            };
            if (galleryImageDeleteModel.GalleryId > 1)
            {
                var _oldData = _db.MstGallery.Where(x => x.GalleryId.Equals(galleryImageDeleteModel.GalleryId) && x.IsActive == true).FirstOrDefault();
                if (_oldData != null)
                {
                    _oldData.Image = null;
                    _oldData.Thumbnail = null;
                    _oldData.ModifiedDate = DateTime.Now;
                    _oldData.Modifiedby = galleryImageDeleteModel.UserId;

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
