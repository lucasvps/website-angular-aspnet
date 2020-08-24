using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetWebApi.Models
{
    [Table("Cliente")]
    public class Cliente : BaseModelo
    {
        [Required]
        public String Name { get; set; }

        [Required]
        [Index(IsUnique =true)]
        [StringLength(450)]
        public String Email { get; set; }

        public List<Pedido> Orders { get; set; }



    }
}