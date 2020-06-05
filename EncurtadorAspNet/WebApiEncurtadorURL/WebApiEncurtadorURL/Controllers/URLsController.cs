using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiEncurtadorURL.Services;

namespace WebApiEncurtadorURL.Controllers
{
    public class URLsController : ApiController
    {

        // GET api/<controller>/5
        public object Get(string alias)
        {

            var encurtadorService = new EncurtadorFacadeService();
            var mensagem = string.Empty;

            var retorno = encurtadorService.GetURL(alias);
            if (retorno)
            {
                mensagem = encurtadorService.Retorno;
                return Redirect(encurtadorService.Retorno);
            }
            else 
            {
                mensagem = encurtadorService.Retorno;
            }

            return mensagem;
        }

        // POST api/<controller>
        public string Post(string url, string alias = null)
        {
            var encurtadorService = new EncurtadorFacadeService();
            var mensagem = string.Empty;

            var retorno = encurtadorService.AddURL(url, alias);
            
            return JsonConvert.SerializeObject(encurtadorService.Retorno);

        }

        
    }
}