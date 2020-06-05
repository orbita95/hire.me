using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebApiEncurtadorURL.App_Start
{
    public class NhibernateContext
    {
        public static ISession GetSession()
        {
            var configuration = new Configuration();
            var configurationPath = HttpContext.Current.Server.MapPath(@"~/App_Start/Nhibernate.cfg.xml");
            configuration.Configure(configurationPath);


            configuration.AddAssembly(Assembly.Load("WebApiEncurtadorURL"));
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}