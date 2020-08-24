using AspNetWebApi.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetWebApi.Controllers
{
    public class ProdutoPedidosController : ApiController
    {
        public class NewProductToOrder
        {
            public long ProdutoId { get; set; }
            public long PedidoId { get; set; }

        }

        [HttpPost]
        [Route("api/produtopedido")]
        public void Post(NewProductToOrder newProductToOrder)
        {

            using(var context = new Contexto())
            {
                var pedidoModelo = context.Pedidos
                    .Where(x => x.Id == newProductToOrder.PedidoId)
                    .Single();

                var produtoModelo = context.Produtos
                    .Where(x => x.Id == newProductToOrder.ProdutoId)
                    .Single();

                var ProdutoPedidoModelo = new Models.ProdutoPedido()
                {
                    Produto = produtoModelo,
                    Pedido = pedidoModelo
                };

                context.ProdutoPedidos.Add(ProdutoPedidoModelo);
                context.SaveChanges();
            }
        }
    }
}
