using AspNetWebApi.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
        public HttpResponseMessage Post(NewProductToOrder newProductToOrder)
        {

            try
            {
                using (var context = new Contexto())
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
                    var message = string.Format("Sucesso");
                    return Request.CreateErrorResponse(HttpStatusCode.OK, message);
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var errors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in errors.ValidationErrors)
                    {

                        var message = validationError.ErrorMessage;
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
                    }
                }
            }

            var error = string.Format("Alguma coisa deu errado, tente novamente!");
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, error);
        }
    }
}
