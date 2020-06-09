using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiEncurtadorURL.Util
{
    public class UrlWebResp
    {
        public string tempoOperacao { get; set; }
        public string url { get; set; }
        public string errorcode { get; set; }
        public string errormessage { get; set; }
        public string shorturl { get; set; }
        public string alias { get; set; }
    }
}