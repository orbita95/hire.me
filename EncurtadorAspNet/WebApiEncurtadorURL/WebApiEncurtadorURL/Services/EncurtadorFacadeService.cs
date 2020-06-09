
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
using System.Security.Cryptography;
using System.Diagnostics;

namespace WebApiEncurtadorURL.Services
{
    public class EncurtadorFacadeService
    {
        private RegistroUrlRepository registroUrlRepository;
        private UnitOfWork unitOfWork;
        public UrlWebResp retorno { get; private set; }

        public EncurtadorFacadeService()
        {
            var session = NhibernateContext.GetSession();
            registroUrlRepository = new RegistroUrlRepository(session);
            unitOfWork = new UnitOfWork(session);
        }

  
        public bool GetURL(string alias) 
        {
            this.retorno = new UrlWebResp();

            try 
            {
                RegistroURL url = registroUrlRepository.FindByALias(alias);

                if (url is null)
                {
                    retorno.errorcode = "002";
                    retorno.errormessage = "SHORTENED URL NOT FOUND";
                    
                    return false;
                }
                else 
                {
                    url.QuantidadeAcessos++;
                    registroUrlRepository.Save(url);

                    retorno.url = url.UrlOriginal;
                    retorno.shorturl = url.UrlEncurtada;

                    
                    return true;
                }



            }
            catch (Exception ex) 
            {
                unitOfWork.Rollback();
                unitOfWork.Dispose();
                retorno.errorcode = "003";
                retorno.errormessage = ex.Message;
                
                return false;

            }

        }

        public UrlWebResp AddURL(string url, string alias) 
        {
            UrlWebResp urlWebResp = new UrlWebResp();

            try
            {
                RegistroURL registroUrl = registroUrlRepository.FindByALias(alias);
                
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                if (registroUrl != null)
                {
                    
                    urlWebResp.errorcode = "001";
                    urlWebResp.errormessage = "CUSTOM ALIAS ALREADY EXISTS";
                    return urlWebResp;
                }
                
                else
                {
                    var urlPadrao = "http://shorturl/";
                    
                    
                    if (String.IsNullOrEmpty(alias))
                    {
                        var inicio = 0;
                        var numeroDeBytes = 1;
                        do
                        {
                            alias = GerarAlias(url, inicio, numeroDeBytes);
                            inicio++;
                            numeroDeBytes++;
                        } while (registroUrlRepository.FindByALias(alias) != null);
                        
                        //executa a operacao de obter um alias a partir de hash
                        //refaz a operacao enquanto o alias nao for unico
                    }

                    urlWebResp.shorturl = urlPadrao + alias;
                    urlWebResp.alias = alias;
                    urlWebResp.url = url;

                    registroUrlRepository.Save(new RegistroURL
                    {
                        Alias = alias,
                        UrlOriginal = url,
                        UrlEncurtada = urlWebResp.shorturl
                    });

                    
                    
                    stopWatch.Stop();
                    urlWebResp.tempoOperacao = stopWatch.ElapsedMilliseconds+" ms";

                    return urlWebResp;
                }

                
                
            }
            catch (Exception ex) 
            {
                unitOfWork.Rollback();
                unitOfWork.Dispose();
                
                urlWebResp.errorcode = "003";
                urlWebResp.errormessage = ex.Message;

                return urlWebResp;
            }

        }


        public string[] GetTenMost(int numeroUrls) 
        {
            UrlWebResp urlWebResp = new UrlWebResp();

            try
            {
                var urls = registroUrlRepository.ToList();
                
                var urlMaisAcessadas = urls.OrderByDescending(u => u.QuantidadeAcessos)
                    .Take(numeroUrls)
                    .Select(a => a.UrlOriginal)
                    .ToArray();

                return urlMaisAcessadas;
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                unitOfWork.Dispose();
                urlWebResp.errorcode = "003";
                urlWebResp.errormessage = ex.Message;
                this.retorno = urlWebResp;

                return null;

            }
        }


        private string GerarAlias(string url, int inicio, int numeroDeBytes) 
        {
            
            var md5hash = MD5.Create();
            var byteArray = Encoding.UTF8.GetBytes(url);

            md5hash.ComputeHash(byteArray, inicio, numeroDeBytes);
            StringBuilder result = new StringBuilder(md5hash.Hash.Length * 2);

            for (int i = 0; i < md5hash.Hash.Length; i++)
                result.Append(md5hash.Hash[i].ToString("x2"));

            var aliasRandom = result.ToString().Substring(0, 7);
            
            return aliasRandom;
        }


        public void Dispose() 
        {
            unitOfWork.Commit();
            unitOfWork.Dispose();
        }

    }
}