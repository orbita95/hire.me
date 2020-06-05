using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiEncurtadorURL.Util
{
    public class UrlWebResp
    {
        

        public string errorcode { get; set; }
        public string errormessage { get; set; }
        public string shorturl { get; set; }
    }
}