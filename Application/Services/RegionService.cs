using Application.Constants;
using Application.Entities;
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
    public class RegionService : BaseService<Region>, IRegionService
    {
        private readonly ISecurityService _securityService;
        public RegionService(IUoW unitOfWork, ISecurityService securityService) : base(unitOfWork)
        {
            _securityService = securityService;
        }

        public async Task<List<RegionDto>> GetBySaleManager(Guid saleManagerId)
        {
            
            var regions = GetAllAsync().Result
                .Select(x=>new RegionDto(x))
                .ToList();


            if (saleManagerId != null && saleManagerId != Guid.Empty)
            {
                var rgionReq = new RegionReq
                {
                    IncludeSaleManagerRegions = true,
                    SaleManagerId = saleManagerId
                };

                var managerRegions = await GetListByFilter<RegionDto>(rgionReq);

                //regions.ForEach(r => r.Checked = managerRegions.Any(y => y.Id == r.Id));
                regions = regions.Where(r => managerRegions.Any(y => y.Id == r.Id)).ToList();
            }

            return regions;
        }

        protected override ISpecification<Region> FilterList(BaseReq<Region> request, ISpecification<Region> spec)
        {
            var req = (RegionReq)request;

            if (req.IncludeSaleManagerRegions)
            {
                spec.Includes.Add(x => x.SaleMangerRegions);

                if (req.SaleManagerId!=null && req.SaleManagerId!=Guid.Empty)
                    spec.Filters.Add(x => x.SaleMangerRegions.Any(y => y.SaleManagerId == req.SaleManagerId));
            }

            return spec;
        }

        public override async Task<IReadOnlyList<Region>> GetAllAsync()
        {
            var regions =  await base.GetAllAsync();
            if (_securityService.IsHaveRole(RoleConstant.Admin))
                return regions;

            var rgionReq = new RegionReq
            {
                IncludeSaleManagerRegions = true,
                SaleManagerId = _securityService.GetCurrUserId()
            };

            var managerRegions = await GetListByFilter(rgionReq);
            return managerRegions;

        }

        public async Task<IReadOnlyList<RegionDto>> GetFullAsync()
        {
            var regions =  await base.GetAllAsync();
            return regions.Select(x => new RegionDto(x)).ToList();
        }

        public async Task<IReadOnlyList<string>> GetBakuCities() 
            => (await _unitOfWork.Repository<CitiesOfBaku>().GetAllAsync())
                                                            .Select(c=>c.Name)
                                                            .ToList();

    }
}
