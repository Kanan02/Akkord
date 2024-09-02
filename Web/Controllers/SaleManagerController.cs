
using Application.Constants;
using Application.Entities;
using Application.Interfaces.IServices;
using Application.Models.Request;
using Application.Models.Request.Ui;
using Application.Models.Response;
using Application.Models.Response.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Controllers.Base;

namespace Web.Controllers
{
    [Authorize] //(Roles = RoleConstant.Admin)
    public class SaleManagerController : BaseController<SaleManager>
    {
        private ISaleManagerService _saleManagerService { get; }

        public SaleManagerController(ISaleManagerService saleManagerService) : base(saleManagerService)
        {
            _saleManagerService = saleManagerService;
        }

        public Task<IReadOnlyList<SaleManagerDto>> GetListByFilter([FromBody] SaleManagerUi request)
        {
            var req = new SaleManagerReq(request);
            return _service.GetListByFilter<SaleManagerDto>(req);
        }


        public  Task<SaleManager> SaveManager([FromBody] SaleManagerInsertUi entity)
        {
            var manger = entity.GetManager();
            return _saleManagerService.Save(manger);
        }

        public Task<ApiResponse> ReplaceSalePoints([FromBody] ManagerPointReplacementUi req)
            => _saleManagerService.ReplaceSalePoints(req);

    }
}
