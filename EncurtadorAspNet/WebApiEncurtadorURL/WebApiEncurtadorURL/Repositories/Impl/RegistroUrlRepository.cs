using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiEncurtadorURL.Models;

namespace WebApiEncurtadorURL.Repositories.Impl
{
    public class RegistroUrlRepository : Repository<RegistroURL>, IRegistroUrlRepository
    {
        public RegistroUrlRepository(ISession session) 
        {
            s = session;
        }

        public RegistroURL FindByALias(string alias)
        {
            var hql = "from RegistroURL as r where r.Alias =:a";
            return s.CreateQuery(hql)
                .SetParameter("a", alias)
                .UniqueResult<RegistroURL>();
        }

        public IList<RegistroURL> FindByUrlOriginal(string urlOriginal)
        {
            var hql = "from RegistroURL as r where r.UrlOriginal =:u";
            return s.CreateQuery(hql)
                .SetParameter("u", urlOriginal)
                .List<RegistroURL>();
        }


    }
}