using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetWebApi.Models
{
    [Table("Mensagem")]
    public class Mensagem : BaseModelo
    {
        [Required]
        public string Descricao { get; set; }

        [Required]
        public DateTime DataHora { get; set; }

        [Required]
        public Contato Contato { get; set; }
    }

}