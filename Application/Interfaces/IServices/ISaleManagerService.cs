using Application.Entities;
using Application.Interfaces.IServices.Base;
using Application.Models.Request.Ui;
using Application.Models.Response.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface ISaleManagerService : IBaseService<SaleManager>
    {
        Task<ApiResponse> ReplaceSalePoints(ManagerPointReplacementUi req);
    }
}
