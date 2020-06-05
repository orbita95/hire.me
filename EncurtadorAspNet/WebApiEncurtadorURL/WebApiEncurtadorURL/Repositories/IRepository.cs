using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiEncurtadorURL.Repositories
{
    interface IRepository<TEntity> where TEntity : class
    {
        void Save(TEntity entity);
        TEntity GetById(int entityId);
        IList<TEntity> ToList();
    }
}
