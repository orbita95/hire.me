using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiEncurtadorURL.Repositories
{
    interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
