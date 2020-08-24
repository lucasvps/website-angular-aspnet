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

namespace AspNetWebApi.Controllers
{
    public class PedidosController : ApiController
    {
        
        public class Pedido
        {
            public long IdClient { get; set; }

            public long Number { get; set; }
            public double FinalCost { get; set; }

            public string Date { get; set; }
            public double Discount { get; set; }
            public double Cost { get; set; }

        }

        // Todos os produtos de um pedido
        [HttpGet]
        [Route("api/pedidos/{id}/produtos")]
        public List<Produto> Produtos(long id)
        {
            using (var contexto = new Contexto())
            {
                var produtosProxy = new List<Produto>();

                var pedidoModelo = contexto.ProdutoPedidos
                    .Include(x => x.Produto)
                    .Where(x => x.PedidoId == id)
                    .ToList();

                foreach (var pedidoModelos in pedidoModelo)
                {
                    var produtoProxy = new Produto()
                    {

                        Name = pedidoModelos.Produto.Name,
                        Description = pedidoModelos.Produto.Description,
                        Value = pedidoModelos.Produto.Value,
                        Id = pedidoModelos.Produto.Id

                    };

                    produtosProxy.Add(produtoProxy);
                }

                return produtosProxy;
            }
        }

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
        }


        public class NewOrder
        {

            public long IdClient { get; set; }
            public long Number { get; set; }
            public double Cost { get; set; }

            public double Discount { get; set; }

        }

        [HttpPost]
        public long Post(NewOrder newOrder)
        {
            using (var context = new Contexto())
            {
                var clientModel = context.Clientes.Where(x => x.Id == newOrder.IdClient).Single();

                var orderModel = new Models.Pedido()
                {
                    Number = (context.Pedidos.Count() + 1000),
                    Cost = newOrder.Cost,
                    Cliente = clientModel,
                    Discount = newOrder.Discount,
                    FinalCost = (newOrder.Cost > newOrder.Discount) ? (newOrder.Cost - newOrder.Discount) : newOrder.Cost,
                    Date = DateTime.Now.ToString(),
                };

                context.Pedidos.Add(orderModel);
                context.SaveChanges();

                return orderModel.Id;
            }

            
        }
    }
}
