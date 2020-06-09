using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiEncurtadorURL.Models
{
    public class RegistroURL : IEntity
    {
        private int id;
        private string urlOriginal;
        private string alias;
        private string urlEncurtada;
        private int quantidadeAcessos;

        public virtual int Id { get => id; set => id = value; }
        public virtual string UrlOriginal { get => urlOriginal; set => urlOriginal = value; }
        public virtual string Alias { get => alias; set => alias = value; }
        public virtual string UrlEncurtada { get => urlEncurtada; set => urlEncurtada = value; }
        public virtual int QuantidadeAcessos { get => quantidadeAcessos; set => quantidadeAcessos = value; } 
    }
}