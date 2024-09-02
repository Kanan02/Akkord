using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Entities.Base;
using Application.Interfaces.IServices.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Base
{
    [Authorize]
    public class BaseController<TEntity> : ControllerBase where TEntity : class
    {
        public IBaseService<TEntity> _service { get;  }

        public BaseController(IBaseService<TEntity> service)
        {
            _service = service;
        }

        public virtual Task<IReadOnlyList<TEntity>> GetAll() => _service.GetAllAsync();

        public virtual Task<TEntity> Save([FromBody] TEntity entity) => _service.Save(entity);
    }
}
