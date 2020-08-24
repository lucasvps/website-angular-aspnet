using AspNetWebApi.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetWebApi.Controllers
{
    public class ProductsController : ApiController
    {

        public class Produto
        {
            public long  Id { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public double Value { get; set; }


        }

        [HttpGet]
        public List<Produto> Get()
        {
            using(var context = new Contexto())
            {
                var productsModel = context.Produtos.ToList();
                var productsList = new List<Produto>();

                foreach(var productModel in productsModel)
                {
                    var product = new Produto()
                    {
                        Id = productModel.Id,
                        Name = productModel.Name,
                        Description = productModel.Description,
                        Value = productModel.Value
                    };


                    productsList.Add(product);
                }

                return productsList;
            }
        }

        [HttpGet]
        public Produto Get(long id)
        {
            using (var context = new Contexto())
            {
                var productModel = context.Produtos
                    .Where(x => x.Id == id)
                    .Single();


                var product = new Produto()
                {
                    Id = productModel.Id,
                    Name = productModel.Name,
                    Description = productModel.Description,
                    Value = productModel.Value

                };

                return product;
            }
        }


        public class NewProduct
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public double Value { get; set; }


        }

        [HttpPost]
        public void Post(NewProduct newProduct)
        {
            using (var context = new Contexto())
            {
                var productModel = new Models.Produto()
                {
                    Name = newProduct.Name,
                    Description = newProduct.Description,
                    Value = newProduct.Value
                };

                context.Produtos.Add(productModel);
                context.SaveChanges();
            }
        }
    }
}
