using Application.Interfaces.IRepository.Base;
using Application.Interfaces.IUoW;
using Infrastructure.Data.Context;
using Infrastructure.Repository.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.UntOfWork
{
    public class UoW : IUoW
    {
        private readonly AkkordDbContext entities = null;
        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UoW(AkkordDbContext entities)
        {
            this.entities = entities;
        }
        public IRepository<T> Repository<T>() where T : class
        {
            if (repositories.ContainsKey(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IRepository<T>;
            }

            IRepository<T> repo = new Repository<T>(entities);
            repositories.Add(typeof(T), repo);
            return repo;
        }

        public async Task SaveChangesAsync() => await entities.SaveChangesAsync();
        
    }
}
