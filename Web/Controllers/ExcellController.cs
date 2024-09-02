using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Constants;
using Application.Interfaces.ICommon;
using Application.Interfaces.IServices;
using Application.Models.Request;
using Application.Models.Request.Ui;
using Application.Models.Response.Excell;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [Authorize]
    public class ExcellController : ControllerBase
    {
        ISalePointService _salePointService;
        IExcellService _excellService;
        
        public ExcellController(ISalePointService salePointService, IExcellService excellService)
        {
            _salePointService = salePointService;
            _excellService = excellService;
        }

        public async Task<IActionResult> ExportSalePoints([FromBody] SalePointUi request)
        {
            var req = new SalePointReq(request);
            var list = await _salePointService.GetListByFilter<SalePointExport>(req);
            return GetListExcellContent(list);
        }

        private FileStreamResult GetListExcellContent<T>(IReadOnlyList<T> list)
        {
            var streamContent = _excellService.ExportList(list,"Sale_points");
            return File(streamContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
