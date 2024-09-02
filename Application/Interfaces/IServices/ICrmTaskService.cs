using Application.Entities;
using Application.Interfaces.IServices.Base;
using Application.Models.Request.Ui;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface ICrmTaskService : IBaseService<CrmTask>
    {
        Task<CrmtaskDetailUi> GetDetail(int taskId);
        Task<CrmtaskDetailUi> Close(CrmtaskDetailUi req);
    }
}
