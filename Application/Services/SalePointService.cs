using Application.Config;
using Application.Constants;
using Application.Entities;
using Application.Exceptions;
using Application.Helpers;
using Application.Interfaces.IServices;
using Application.Interfaces.IServices.Security;
using Application.Interfaces.IUoW;
using Application.Models.Request;
using Application.Models.Request.Base;
using Application.Models.Response;
using Application.Services.Base;
using Application.Spesifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SalePointService : BaseService<SalePoint>, ISalePointService
    {
        private readonly ISecurityService _securityService;
        private readonly IRegionService _regionService;
        public SalePointService(IUoW unitOfWork, ISecurityService securityService,IRegionService regionService) : base(unitOfWork)
        {
            _securityService = securityService;
            _regionService = regionService;
        }

        public async override Task<SalePoint> Save(SalePoint entity)
        {
            ValidateSaveByRole(entity);

            //entity.ValidateSave();
            //await ValidateByManagerRegion(entity.RegionId);
            if(!_securityService.IsHaveRole(RoleConstant.Admin))
                entity.SaleManagerId = _securityService.GetCurrUserId();

            if (entity.Id == 0)
            {
                if (entity.Photos != null && entity.Photos.Count() > 0)
                {
                    entity.Photos.ForEach(p => p.Photo = new Photo { Base64 = p.Base64 });
                    PhotoHelper.SaveFiles(entity.Photos.Select(p => p.Photo).ToList());

                }

                //PhotoHelper.SaveToFile(entity.Photo);
                return await base.Save(entity);
            }
            else
                return await UpdateSalePoint(entity);

        }

        private void ValidateSaveByRole(SalePoint entity)
        {
            if (_securityService.IsHaveRole(RoleConstant.Admin))
                entity.ValidateAdminSave();
            else
                entity.ValidateSave();
        }

        private  async Task ValidateByManagerRegion(int regionId)
        {
            if (_securityService.IsHaveRole(RoleConstant.Admin))
                return;

            var managerId = _securityService.GetCurrUserId();
            var userRegions = await _regionService.GetBySaleManager(managerId);

            if (!userRegions.Any(x => x.Id == regionId))
                throw new AkkordException("invalid_sale_manager_region");

        }

        private async Task<SalePoint> UpdateSalePoint(SalePoint uiPoint)
        {

            var dbPoint = await GetByIncludes(uiPoint.Id);

            uiPoint.SalePointPortfolioes.SetListPropValue(uiPoint.Id, nameof(SalePointPortfolio.SalePointId));
            uiPoint.SalePointSellers.SetListPropValue(uiPoint.Id, nameof(SalePointSeller.SalePointId));

            await RelationSaveHelper.SaveOnaToManyList(uiPoint.SalePointPortfolioes, dbPoint.SalePointPortfolioes, nameof(SalePointPortfolio.PortfolioId), _unitOfWork);
            await RelationSaveHelper.SaveOnaToManyList(uiPoint.SalePointSellers, dbPoint.SalePointSellers, nameof(SalePointSeller.Id), _unitOfWork,true);

            await SaveTaskPhoto(uiPoint, dbPoint);
            _unitOfWork.Repository<SalePoint>().Update(uiPoint, dbPoint);
            await SaveUoW();
            return dbPoint;
        }

        private async Task<SalePoint> GetByIncludes(int salePointId)
        {
            var salePointReq = new SalePointReq
            {
                IncludePortfolios = true,
                IncludeSellers = true,
                IncludePhoto = true,
                IncludePhotos = true,
                Value = new SalePoint
                {
                    Id = salePointId
                }
            };

            var result = await GetListByFilter(salePointReq);
            return result.FirstOrDefault();
        }

        protected override ISpecification<SalePoint> FilterList(BaseReq<SalePoint> request, ISpecification<SalePoint> spec )
        {
            var req = (SalePointReq)request;
            var value = req.Value;

            ValidateFilterByManager(req);

            if (req.IncludePortfolios)
            {
                var portfolioInclude = $"{nameof(SalePoint.SalePointPortfolioes)}.{nameof(SalePointPortfolio.Portfolio)}";
                spec.IncludeStrings.Add(portfolioInclude);
            }

            if (req.IncludePhotos)
            {
                var portfolioInclude = $"{nameof(SalePoint.Photos)}.{nameof(SalePointPhoto.Photo)}";
                spec.IncludeStrings.Add(portfolioInclude);
            }

            if (req.IncludeSellers)
                spec.Includes.Add(x => x.SalePointSellers);

            if (req.IncludePhoto)
                spec.Includes.Add(x => x.Photo);

            if (req.IncludeRegion)
                spec.Includes.Add(x => x.Region);

            if (req.IncludeSaleManager)
                spec.Includes.Add(x => x.SaleManager);

            if (value!=null)
            {
                if (value.Id > 0)
                    spec.Filters.Add(x => x.Id == value.Id);

                if (value.RegionId > 0)
                    spec.Filters.Add(x => x.RegionId == value.RegionId);

                if (req.RegionIds!=null && req.RegionIds.Count > 0)
                    spec.Filters.Add(x => req.RegionIds.Any(r => r==x.RegionId));

                if (value.SaleManagerId != Guid.Empty)
                    spec.Filters.Add(x => x.SaleManagerId == value.SaleManagerId);


                if (!string.IsNullOrEmpty(value.CodeName))
                    spec.Filters.Add(x => x.CodeName.Contains(value.CodeName.Trim()));


                if (value.SaleSegment > 0)
                    spec.Filters.Add(x => x.SaleSegment == value.SaleSegment);

                if (value.Status > 0)
                    spec.Filters.Add(x => x.Status == value.Status);
            }

            //if(req.Pager!=null)
            //    spec.ApplyPaging(req.Pager.Skip, req.Pager.PageSize);

            spec.ApplyOrderByDescending((x) => x.Id);

            return spec;
        }

        public async Task<SalePoint> Get(int id)
        {
            var req = new SalePointReq
            {
                IncludeSellers = true,
                IncludePortfolios = true,
                IncludePhotos = true,
                IncludePhoto = true,
                Value = new SalePoint
                {
                    Id = id
                }
            };

            var result = (await GetListByFilter(req, false))?.FirstOrDefault();

            if (result != null)
                result.Photos?.ForEach(p =>
                {
                    p.Image = $"{ProjectSetting.PhotoPath}{p.Photo.Src}";
                    p.Photo = null;

                });
            else
                result.Photos = new List<SalePointPhoto>();


            result.Image = result != null && result.Photo != null 
                ? $"{ProjectSetting.PhotoPath}{result.Photo.Src}" : null;
            return result;
        }

        private void ValidateFilterByManager(SalePointReq req)
        {
            if (_securityService.IsHaveRole(RoleConstant.Admin))
                return;

            req = req ?? new SalePointReq { Value =  new SalePoint() };
            req.Value = req.Value ?? new SalePoint();

            req.Value.SaleManagerId = _securityService.GetCurrUserId();
        }

        public async Task<List<SalePointDto>> GetFull()
        {
            var salePoints = await base.GetAllAsync();
            return salePoints.Select(x => new SalePointDto().SetDto(x)).ToList();
        }

        private async Task SaveTaskPhoto(SalePoint uiSalePoint, SalePoint salePoint)
        {
            var uiPhotos = uiSalePoint.Photos.Select(p => new SalePointPhoto { PhotoId = p.PhotoId, SalePointId = uiSalePoint.Id }).ToList();
            var photoList = RelationSaveHelper.GetUpdatedList(uiPhotos, salePoint.Photos, nameof(SalePointPhoto.PhotoId));


            var deletedPhoto = salePoint.Photos.Where(p => photoList.deleted.Any(x => x.PhotoId == p.PhotoId))
                .Select(x => x.Photo)
                .ToList();
            var savedPhoto = uiSalePoint.Photos.Where(p => !photoList.deleted.Any(x => x.PhotoId == p.PhotoId)).Select(x => new Photo
            {
                Id = x.PhotoId,
                Src = x.Photo!=null ?x.Photo.Src : null,
                Base64 = x.Base64
            }).ToList();

            List<Photo> newPhotos = new List<Photo>();

            foreach (var p in savedPhoto)
            {
                if (p.Id == 0)
                {
                    p.SalePointPhoto = new SalePointPhoto { SalePointId = salePoint.Id };
                    newPhotos.Add(p);
                }
                else
                    p.Src = salePoint.Photos.FirstOrDefault(x => x.PhotoId == p.Id)?.Photo.Src;
            }

            await _unitOfWork.Repository<Photo>().AddAllAsync(newPhotos);
            _unitOfWork.Repository<Photo>().RemoveAll(deletedPhoto);

            PhotoHelper.DeleteFiles(deletedPhoto);
            PhotoHelper.SaveFiles(savedPhoto);

        }
    }
}
