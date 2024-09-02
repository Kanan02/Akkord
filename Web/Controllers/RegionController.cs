using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Constants;
using Application.Entities;
using Application.Interfaces.IServices;
using Application.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;

namespace Web.Controllers
{
    [Authorize]
    public class RegionController : BaseController<Region>
    {
        private IRegionService _regionService { get; }
        public RegionController(IRegionService regionService) : base(regionService)
        {
            _regionService = regionService;
        }

        public Task<List<RegionDto>> GetBySaleManager(Guid id) => _regionService.GetBySaleManager(id);

        [Authorize(Roles = RoleConstant.Admin)]
        public override Task<Region> Save([FromBody] Region entity) => base.Save(entity);

        public Task<IReadOnlyList<string>> GetCities(Guid id) => _regionService.GetBakuCities();
        public Task<IReadOnlyList<RegionDto>> GetFull() => _regionService.GetFullAsync();

        

    }
}
