using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetWebApi.Models
{
    [Table("ProdutoPedido")]
    public class ProdutoPedido
    {
        public long ProdutoId { get; set; }
        public long PedidoId { get; set; }

        public Pedido Pedido { get; set; }
        public Produto Produto { get; set; }

    }
}