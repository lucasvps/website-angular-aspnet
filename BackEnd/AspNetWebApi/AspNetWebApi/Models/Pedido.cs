using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetWebApi.Models
{
    [Table("Pedido")]
    public class Pedido : BaseModelo
    {
        [Required]
        public long Number { get; set; }

        [Required]
        public double FinalCost { get; set; }

        [Required]
        public Cliente Cliente { get; set; }

        
        public List<ProdutoPedido> ProdutoPedidos { get; set; }

        [Required]
        public double Cost { get; set; }
        public double Discount { get; set; }

        [Required]
        public string Date { get; set; }


    }
}