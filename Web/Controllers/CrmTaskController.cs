using System;
using System.Collections.Generic;
using System.Linq;
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
using Web.Controllers.Base;

namespace Web.Controllers
{
    [Authorize]
    public class CrmTaskController : BaseController<CrmTask>
    {
        private readonly ICrmTaskService _crmTaskService;
        public CrmTaskController(ICrmTaskService service,
            ICrmTaskService crmTaskService) : base(service) 
        {
            _crmTaskService = crmTaskService;
        }

        public Task<IReadOnlyList<CrmTaskDto>> GetListByFilter([FromBody] CrmTaskUi request)
        {

            var req = new CrmTaskReq(request);
            req.IncludeSalePoint = true;
            req.IncludeNotification = true;
            return _service.GetListByFilter<CrmTaskDto>(req);
        }

        public Task<CrmtaskDetailUi> GetDetail(int id) => _crmTaskService.GetDetail(id);

        public Task<CrmtaskDetailUi> Close([FromBody] CrmtaskDetailUi req) => _crmTaskService.Close(req);

    }
}
