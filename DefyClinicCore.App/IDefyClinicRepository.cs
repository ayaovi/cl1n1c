
using DefyClinicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefyClinicCore.App
{
    public interface IDefyClinicRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        TEntity GetById(object id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> Find(Func<TEntity, bool> predicate);
    }
}
