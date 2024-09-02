using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Interfaces.Base
{
    public class IBaseService<TEntity,TId> where TEntity  : Base<TId>
    {

    }
}
