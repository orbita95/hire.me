using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{



    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            urlOriginal_lbl.Text = "";
            shortUrl_lbl.Text = "";
            alias_lbl.Text = "";
            tempoOperacao_lbl.Text = "";
            error_lbl.Text = "";
        }

       

        protected void Unnamed_Click1(object sender, EventArgs e)
        {
            var response = PostUrlEncurtada(urlPadrao_tb.Text, alias_tb.Text);

            if (String.IsNullOrEmpty(response.errorcode))
            {
                alias_lbl.Text = response.alias;
                shortUrl_lbl.Text = response.shorturl;
                tempoOperacao_lbl.Text = response.tempoOperacao;
                urlOriginal_lbl.Enabled = false;
                error_lbl.Enabled = false;
                alias_lbl.Enabled = false;
            }
            else 
            {
                error_lbl.Enabled = true;
                error_lbl.Text = response.errormessage;
            }
            
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            var response = GetUrlEncurtada(alias_se_tb.Text);

            if (String.IsNullOrEmpty(response.errorcode))
            {
                

                shortUrl_lbl.Enabled = false;
                tempoOperacao_lbl.Enabled = false;
                urlOriginal_lbl.Enabled = false;
                error_lbl.Enabled = false;
                alias_lbl.Enabled = false;

                urlOriginal_lbl.Text = response.url;
            }
            else
            {
                error_lbl.Enabled = true;
                error_lbl.Text = response.errormessage;
            }
            
            
        }

        private UrlWebReq PostUrlEncurtada(string url, string alias)
        {
            var urlPost = new StringBuilder("https://localhost:44368/api/URLs?");
            
            if (String.IsNullOrEmpty(alias))
            {
                urlPost.AppendFormat("url={0}", url);
            }
            else
            {
                urlPost.AppendFormat("url={0}&alias={1}", url, alias);
            }

            var retorno = makePostRequest(urlPost.ToString());
            return JsonConvert.DeserializeObject<UrlWebReq>(retorno.ToString());

        }

        private UrlWebReq GetUrlEncurtada(string alias)
        {
            var urlPost = new StringBuilder("https://localhost:44368/api/URLs?");
            urlPost.AppendFormat("alias={0}",  alias);

            var retorno = makeGetRequest(urlPost.ToString());
            return JsonConvert.DeserializeObject<UrlWebReq>(retorno.ToString());

        }

        private object makeGetRequest(string urlGet) {
            var requisicaoWeb = (HttpWebRequest)WebRequest.Create(urlGet);
            requisicaoWeb.AutomaticDecompression = DecompressionMethods.GZip;


            requisicaoWeb.CookieContainer = new CookieContainer();
            requisicaoWeb.Method = "GET";
            

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            using (var resposta = (HttpWebResponse)requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                return objResponse;



            }
        }


        private object makePostRequest( string urlPost)
        {
            var requisicaoWeb = (HttpWebRequest)WebRequest.Create(urlPost);
            requisicaoWeb.AutomaticDecompression = DecompressionMethods.GZip;


            requisicaoWeb.CookieContainer = new CookieContainer();
            requisicaoWeb.Method = "POST";
            requisicaoWeb.ContentLength = 0;

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            using (var resposta = (HttpWebResponse)requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                return objResponse;



            }
        }


    }


    internal class UrlWebReq
    {
        public string tempoOperacao { get; set; }
        public string url { get; set; }
        public string errorcode { get; set; }
        public string errormessage { get; set; }
        public string shorturl { get; set; }
        public string alias { get; set; }
    }

}