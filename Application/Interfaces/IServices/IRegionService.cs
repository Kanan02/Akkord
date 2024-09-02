using Application.Entities;
using Application.Interfaces.IServices.Base;
using Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IRegionService : IBaseService<Region>
    {
        Task<List<RegionDto>> GetBySaleManager(Guid saleManagerId);
        Task<IReadOnlyList<string>> GetBakuCities();
        Task<IReadOnlyList<RegionDto>> GetFullAsync();
    }
}
