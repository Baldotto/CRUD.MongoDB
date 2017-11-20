using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace VB.CRUD.Domain
{
    [DataContract]
    public class Post : Entity
    {

        public Post()
        {
            DataCriacao = DateTime.Now;
            DataUltimaModificacao = DateTime.Now;
        }

        [DataMember]
        public string Titulo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimaModificacao { get; set; }
        [DataMember]
        public string Conteudo { get; set; }

    }
}
