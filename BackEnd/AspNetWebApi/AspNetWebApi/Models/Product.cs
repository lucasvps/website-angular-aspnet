using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetWebApi.Models
{
    [Table("Produto")]
    public class Product : BaseModelo
    {

        
        
        public string Image { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }


        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, Double.PositiveInfinity)]
        public double Value { get; set; }

        public Product(){}

        public Product(string Description, double Value, string Image)
        {
           
            this.Description = Description;
            this.Value = Value;
            this.Image = Image;
        }
    }
}