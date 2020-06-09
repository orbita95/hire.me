using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiEncurtadorURL.Repositories.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private ISession session;
        private ITransaction transaction;

        public UnitOfWork(ISession _session)
        {
            session = _session;
            transaction = session.BeginTransaction();
        }

        public void Commit()
        {
            if (transaction.IsActive)
            {
                transaction.Commit();
            }

            
        }

        public void Dispose()
        {
            if (session.IsOpen)
            {
                session.Close();
            }
        }

        public void Rollback()
        {
            if (transaction.IsActive)
            {
                transaction.Rollback();
            }
        }
    }
}