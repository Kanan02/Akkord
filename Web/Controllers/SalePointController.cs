using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Entities;
using Application.Interfaces.IServices;
using Application.Interfaces.IServices.Base;
using Application.Models.Request;
using Application.Models.Request.Ui;
using Application.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Controllers.Base;

namespace Web.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [Authorize]
    public class SalePointController : BaseController<SalePoint>
    {
        private readonly ISalePointService _salePointService;
        private readonly ILogger<SalePointController> _logger;
        public SalePointController(ISalePointService salePointService, ILogger<SalePointController> logger) : base(salePointService)
        {
            _salePointService = salePointService;
            _logger = logger;
        }

        public Task<IReadOnlyList<SalePointDto>> GetLitByFilter([FromBody] SalePointUi request)
        {
            var req = new SalePointReq(request);
            return _service.GetListByFilter<SalePointDto>(req);
        }

        public Task<SalePoint> Get(int id) => _salePointService.Get(id);

        public override Task<SalePoint> Save([FromBody] SalePoint entity)
        {
            string entityAsJson = JsonSerializer.Serialize(entity);
            _logger.LogInformation($"Sale point save sent obj {entityAsJson}");
            return   base.Save(entity);
           

        }


        public Task<List<SalePointDto>> GetFull() => _salePointService.GetFull();


    }
}
