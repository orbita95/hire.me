using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiEncurtadorURL.Models;

namespace WebApiEncurtadorURL.Repositories
{
    interface IRegistroUrlRepository : IRepository<RegistroURL>
    {
        RegistroURL FindByALias(string alias);
        IList<RegistroURL> FindByUrlOriginal(string urlOriginal);

    }
}
