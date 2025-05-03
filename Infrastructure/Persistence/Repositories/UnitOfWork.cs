using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
           //Get Type Name
           var typeName = typeof(TEntity).Name;
           if(_repositories.TryGetValue(typeName, out object? value))
                return(IGenericRepository<TEntity, TKey>) value;
           else
            {
                //Create Object
                var Repo = new GenericRepository<TEntity, TKey>(_dbContext);
                //Store In Dic
                _repositories["typeName"] = Repo;
                //Return Object
                return Repo;
            }
        }

        public async Task<int> SaveChangesAsync() =>await _dbContext.SaveChangesAsync();
    }
}
