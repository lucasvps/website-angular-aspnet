using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetWebApi.Models
{
    [Table("Contato")]
    public class Contato : BaseModelo
    {
        [Required]
        public string Nome { get; set; }

        public List<Mensagem> Mensagens { get; set; }
    }
}