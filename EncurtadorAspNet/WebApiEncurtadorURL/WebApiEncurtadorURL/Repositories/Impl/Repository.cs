using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiEncurtadorURL.Repositories.Impl
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected ISession s;

        public TEntity GetById(int entityId)
        {
            throw new NotImplementedException();
        }

        public void Save(TEntity entity)
        {
            s.Save(entity);
        }

        public IList<TEntity> ToList()
        {
            return s.CreateQuery("from " + typeof(TEntity) ).List<TEntity>();
        }
    }
}