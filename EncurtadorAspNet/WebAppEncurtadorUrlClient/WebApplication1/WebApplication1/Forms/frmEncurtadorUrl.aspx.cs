using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        }

       

        protected void Unnamed_Click1(object sender, EventArgs e)
        {
            var response = PostUrlEncurtada(urlPadrao_tb.Text, alias_tb.Text);
            retorno_lbl.Text = response.ToString();
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            var response = GetUrlEncurtada(alias_tb.Text);

            retorno_lbl.Text = response.ToString();
        }

        private object PostUrlEncurtada(string url, string alias)
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

            return makePostRequest(urlPost.ToString());


        }

        private object GetUrlEncurtada(string alias)
        {
            var urlPost = new StringBuilder("https://localhost:44368/api/URLs?");
            urlPost.AppendFormat("alias={0}",  alias);

            return makeGetRequest( urlPost.ToString());


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


    internal class RetornoUrl
    {
        public string Description { get; set; }
        public string UrlEncurtada { get; set; }
        public string UrlOriginal { get; set; }
    }

}