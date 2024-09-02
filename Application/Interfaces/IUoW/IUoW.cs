using Application.Interfaces.IRepository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IUoW
{
    public interface IUoW
    {
        IRepository<T> Repository<T>() where T : class;

         Task SaveChangesAsync();
    }
}
