using Application.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Response.Base
{
    public interface IListFilterDto<TEntity,TDto>
    {
        TDto SetDto(TEntity entity);
    }
}
