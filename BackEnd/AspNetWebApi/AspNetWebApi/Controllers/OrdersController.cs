using AspNetWebApi.Context;
using AspNetWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AspNetWebApi.Controllers
{
    
    public class OrdersController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage Post([FromBody] int[] products, int client, double discount)
        {

            using(var context = new Contexto())
            {
                
                if(!float.IsNaN(client) && !double.IsNaN(discount) && products.Length > 0)
                {
                    {

                        if(discount < 0)
                        {
                            var error = string.Format("O desconto não pode ter um valor negativo!");
                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, error);
                        }

                        Order orderModel = new Order();
                        orderModel.Products = new List<Product>();

                        double calculateCost = 0;

                        foreach (var product in products)
                        {
                            orderModel.Products.Add(context.Products.Find(product));
                            Product productFind = context.Products.Find(product);
                            calculateCost += productFind.Value;
                        }

                        if (discount > calculateCost)
                        {
                            var error = string.Format("O desconto não pode ser maior que o valor do pedido!");
                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, error);

                        }

                        orderModel.Date = DateTime.Now;
                        orderModel.Cliente = context.Clients.Find(client);
                        orderModel.Cost = calculateCost;
                        orderModel.Discount = discount;
                        orderModel.FinalCost = calculateCost - discount;
                        orderModel.Number = (context.Orders.Count() + 1000);


                        context.Orders.Add(orderModel);
                        context.SaveChanges();

                        var message = string.Format("Pedido realizado com sucesso!");
                        return Request.CreateErrorResponse(HttpStatusCode.OK, message);

                    }
                } else
                {
                    var error = string.Format("Alguma coisa deu errado, tente novamente!");
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, error);
                }
            }
        }


        // Todos os produtos de um pedido
        [HttpGet]
        [Route("api/orders/{id}/products")]
        public List<Product> ProductsFromOrder(long id)
        {
            using (var contexto = new Contexto())
            {
                var productsList = new List<Product>();

                var orderModel = contexto.Orders
                    .Where(x => x.Id == id)
                    .First();

                foreach (var products in orderModel.Products)
                {
                    var productModel = new Product()
                    {

                        Name = products.Name,
                        Description = products.Description,
                        Value = products.Value,
                        Id = products.Id

                    };

                    productsList.Add(productModel);
                }

                return productsList;
            }
        }

        /*
        [HttpGet]
        public List<Pedido> Get()
        {
            using (var context = new Contexto())
            {
                var pedidosModel = context.Pedidos.ToList();
                var pedidosList = new List<Pedido>();

                foreach (var pedidoModel in pedidosModel)
                {
                    var pedido = new Pedido()
                    {
                        

                    };

                    pedidosList.Add(pedido);
                }

                return pedidosList;
            }
        }*/
    
    }
}