using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetWebApi.Models
{
    [Table("Pedido")]
    public class Order : BaseModelo
    {

        [Required]
        public long Number { get; set; }

        [Required]
        [Range(1, Double.PositiveInfinity)]
        public double FinalCost { get; set; }

        [Required]
        [ForeignKey("IdClient")]
        public Client Cliente { get; set; }

        public long IdClient { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        [Required]
        [Range(1, Double.PositiveInfinity)]
        public double Cost { get; set; }

        [Range(0, Double.PositiveInfinity)]
        public double Discount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public Order(long Id,double Discount, double Cost, double FinalCost, long Number, List<Product> products, Client Client, DateTime Date)
        {
            this.Id = Id;
            this.Number = Number;
            this.Discount = Discount;
            this.Cost = Cost;
            this.FinalCost = FinalCost;
            this.Cliente = Client;
            this.IdClient = Client.Id;
            this.Date = Date;

            Products = new List<Product>();

            foreach (var product in products)
            {
                this.Products.Add(product);

            }


        }

        public Order() { }
    }
}