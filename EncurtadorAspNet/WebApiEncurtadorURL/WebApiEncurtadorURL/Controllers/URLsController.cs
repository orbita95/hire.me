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
        public IHttpActionResult Get(string alias)
        {

            var encurtadorService = new EncurtadorFacadeService();

            var retorno = encurtadorService.GetURL(alias);
            encurtadorService.Dispose();

            return Ok(encurtadorService.retorno);            
        }

        // POST api/<controller>
        public IHttpActionResult Post(string url, string alias = null)
        {
            var encurtadorService = new EncurtadorFacadeService();
            
            var retorno = encurtadorService.AddURL(url, alias);
            encurtadorService.Dispose();

            return Ok(retorno);

        }

        [HttpGet]
        [Route("api/URLs/{quantidade}/maisacessadas")]
        public IHttpActionResult Get(int quantidade)
        {
            var encurtadorService = new EncurtadorFacadeService();
            var mensagem = string.Empty;

            var urls = encurtadorService.GetTenMost(quantidade);
            encurtadorService.Dispose();

            if (urls != null)
                return Ok(urls);
            else
                return Ok(encurtadorService.retorno);

        }

        [HttpGet]
        [Route("api/URLs/{alias}/ir")]
        public IHttpActionResult GetUrlByAlias(string alias)
        {   
            var encurtadorService = new EncurtadorFacadeService();
            
            var retorno = encurtadorService.GetURL(alias);
            encurtadorService.Dispose();

            if(retorno)
                return Redirect(encurtadorService.retorno.url);
            else
                return Ok(encurtadorService.retorno);
        }


    }
}