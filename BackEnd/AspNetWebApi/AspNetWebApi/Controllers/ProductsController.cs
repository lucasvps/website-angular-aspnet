using AspNetWebApi.Context;
using AspNetWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


namespace AspNetWebApi.Controllers
{


    public class ProductsController : ApiController
    {

        [HttpGet]
        public List<Product> Get()
        {
            using (var context = new Contexto())
            {
                var productsModel = context.Products.ToList();
                var productsList = new List<Product>();

                foreach (var productModel in productsModel)
                {
                    var product = new Product()
                    {
                        Id = productModel.Id,
                        Image = productModel.Image,
                        Description = productModel.Description,
                        Value = productModel.Value
                    };


                    productsList.Add(product);
                }

                return productsList;
            }
        }

        [HttpGet]
        public Product Get(long id)
        {
            using (var context = new Contexto())
            {
                var productModel = context.Products
                    .Where(x => x.Id == id)
                    .Single();


                var product = new Product()
                {
                    Id = productModel.Id,

                    Description = productModel.Description,
                    Value = productModel.Value

                };

                return product;
            }
        }



        public class NewProduct
        {
            public string Image { get; set; }
            public string Description { get; set; }
            public double Value { get; set; }

        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post(NewProduct newProduct)
        {

            try
            {

                if(newProduct.Value <= 0)
                {
                    var err = string.Format("O valor do produto precisa ser maior do que zero!");
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
                }

                if(newProduct.Description.Length < 8 || newProduct.Description.Trim() == "" || newProduct.Description.Trim().Length < 8)
                {
                    var err2 = string.Format("Esta nao é uma descrição válida!");
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err2);
                }

                using (var context = new Contexto())
                {

                    var productModel = new Models.Product()
                    {

                        Image = newProduct.Image,
                        Description = newProduct.Description,
                        Value = newProduct.Value
                    };

                    context.Products.Add(productModel);
                    context.SaveChanges();

                }

                var message1 = string.Format("Produto adicionado com sucesso!");
                return Request.CreateErrorResponse(HttpStatusCode.Created, message1);


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

