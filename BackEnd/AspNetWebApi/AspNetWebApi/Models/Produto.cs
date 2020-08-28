using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetWebApi.Models
{
    [Table("Produto")]
    public class Produto : BaseModelo
    {
        
        public string ImagePath { get; set; }

        //public HttpPostedFileBase ImageFile { get; set; }

        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, Double.PositiveInfinity)]
        public double Value { get; set; }

        public List<ProdutoPedido> ProdutoPedidos { get; set; }


    }
}