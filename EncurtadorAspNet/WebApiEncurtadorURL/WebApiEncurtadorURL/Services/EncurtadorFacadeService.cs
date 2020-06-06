using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using WebApiEncurtadorURL.App_Start;
using WebApiEncurtadorURL.Models;
using WebApiEncurtadorURL.Repositories.Impl;
using WebApiEncurtadorURL.Util;

namespace WebApiEncurtadorURL.Services
{
    public class EncurtadorFacadeService
    {
        private RegistroUrlRepository registroUrlRepository;
        private UnitOfWork unitOfWork;

        public string Retorno { get; private set; }

        public EncurtadorFacadeService()
        {
            var session = NhibernateContext.GetSession();
            registroUrlRepository = new RegistroUrlRepository(session);
            unitOfWork = new UnitOfWork(session);
        }

        public bool GetURL(string alias) 
        {
            try 
            {
                RegistroURL url = registroUrlRepository.FindByALias(alias);

                if (url is null)
                {
                    Retorno = "{'ERR_CODE': '002', 'Description': 'SHORTENED URL NOT FOUND'}";
                    return false;
                }
                else 
                {
                    Retorno = "{'UrlOriginal': '" + url.UrlOriginal + "'}" ;
                    return true;
                }



            }
            catch (Exception ex) 
            {
                unitOfWork.Rollback();
                unitOfWork.Dispose();
                Retorno = "{'ERR_CODE': '003', 'Description': '" + ex.Message+"'}";
                return false;

            }

        }

        public bool AddURL(string urlNova, string alias) 
        {
            try
            {
                RegistroURL url = registroUrlRepository.FindByALias(alias);
                UrlWebResp urlWebResp = new UrlWebResp();
                var response = GerarUrlEncurtada(urlNova, alias);
                urlWebResp = JsonConvert.DeserializeObject<UrlWebResp>(response.ToString());


                if (url != null)
                {
                    Retorno = "{'ERR_CODE': '001', 'Description': 'CUSTOM ALIAS ALREADY EXISTS'}";
                    return false;
                }
                else if (urlWebResp.shorturl is null) 
                {
                    Retorno = "{'ERR_CODE': '" + urlWebResp.errorcode+ "' , 'Description': '" + urlWebResp.errormessage+"'}";
                    return false;
                }
                else
                {
                    var urlEncurtada = "";
                    urlEncurtada = urlWebResp.shorturl;

                    if (String.IsNullOrEmpty(alias))
                    {
                        alias = urlEncurtada.Split('/')[urlEncurtada.Split('/').Length - 1]; //chamada rotina para gerar alias
                    }


                    registroUrlRepository.Save(new RegistroURL
                    {
                        Alias = alias,
                        UrlOriginal = urlNova,
                        UrlEncurtada = urlEncurtada
                    });

                    
                    
                    Retorno = "{'UrlEncurtada': '"+ urlEncurtada + "', 'UrlOriginal': '"+urlNova+"'}" ;

                    Dispose();
                    return true;
                }

            }
            catch (Exception ex) 
            {
                unitOfWork.Rollback();
                unitOfWork.Dispose();
                Retorno = "{'ERR_CODE': '003', 'Description': '" + ex.Message+"'}";
                return false;
            }
        }


        private object GerarUrlEncurtada(string url, string alias) 
        {
            
            // O objetivo do teste é testar sua capacidade de lidar com a complexidade de
            // criar o seu proprio encurtador, a forma que você usou abaixo impossibilita isso.
            // da uma olhada nos objetivos propostos na readme.
            var urlPost = new StringBuilder("https://is.gd/create.php?format=json&");

            if (String.IsNullOrEmpty(alias))
            {
                urlPost.AppendFormat("url={0}",url);
            }
            else {
                urlPost.AppendFormat("shorturl={0}&url={1}", alias, url);
            }

            
            var requisicaoWeb = (HttpWebRequest)WebRequest.Create(urlPost.ToString());
            requisicaoWeb.AutomaticDecompression = DecompressionMethods.GZip;


            requisicaoWeb.CookieContainer = new CookieContainer();
            requisicaoWeb.Method = "POST";
            
            using (var resposta = (HttpWebResponse)requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                return objResponse;



            }
        }


        public void Dispose() 
        {
            unitOfWork.Commit();
            unitOfWork.Dispose();
        }

    }
}
