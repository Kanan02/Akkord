using Application.Entities;
using Application.Interfaces.IServices.Base;
using Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface ISalePointService : IBaseService<SalePoint>
    {
        Task<SalePoint> Get(int id);
        Task<List<SalePointDto>> GetFull();
    }
}
