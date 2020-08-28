using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace AspNetWebApi.Models
{
    [Table("Cliente")]
    public class Cliente : BaseModelo
    {
        [Required]
        public String Name { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [Index(IsUnique = true)]
        [StringLength(450)]
        //[RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Este não é um email válido!")]
        public String Email { get; set; }

        public List<Pedido> Orders { get; set; }



    }
}