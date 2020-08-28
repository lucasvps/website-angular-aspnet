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
        [Range(1, Double.PositiveInfinity)]
        public double FinalCost { get; set; }

        [Required]
        [ForeignKey("IdClient")]
        public Cliente Cliente { get; set; }

        public long IdClient { get; set; }


        public List<ProdutoPedido> ProdutoPedidos { get; set; }

        [Required]
        [Range(1, Double.PositiveInfinity)]
        public double Cost { get; set; }

        [Range(0, Double.PositiveInfinity)]
        public double Discount { get; set; }

        [Required]
        public DateTime Date { get; set; }


    }
}