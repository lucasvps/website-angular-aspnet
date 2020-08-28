using AspNetWebApi.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace AspNetWebApi.Controllers
{
    public class ProductsController : ApiController
    {

        public class Produto
        {

           

            public long  Id { get; set; }

            public string Description { get; set; }

            public double Value { get; set; }

            public string ImagePath { get; set; }



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
                        ImagePath = productModel.ImagePath,
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
                   
                    Description = productModel.Description,
                    Value = productModel.Value

                };

                return product;
            }
        }


        public class NewProduct
        {
            public string ImagePath { get; set; }
            public string Description { get; set; }
            public double Value { get; set; }

           // public HttpPostedFileBase ImageFile { get; set; }


        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {

            try
            {

                using (var context = new Contexto())
                {

                    Dictionary<string, object> dict = new Dictionary<string, object>();

                    var httpRequest = HttpContext.Current.Request;

                    foreach (string file in httpRequest.Files)
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                        var postedFile = httpRequest.Files[file];
                        if (postedFile != null && postedFile.ContentLength > 0)
                        {

                            int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                            IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                            var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                            var extension = ext.ToLower();
                            if (!AllowedFileExtensions.Contains(extension))
                            {

                                var messages = string.Format("Please Upload image of type .jpg,.gif,.png.");

                                dict.Add("error", messages);
                                return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                            }
                            else if (postedFile.ContentLength > MaxContentLength)
                            {

                                var messages = string.Format("Please Upload a file upto 1 mb.");

                                dict.Add("error", messages);
                                return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                            }
                            else
                            {

                                var filePath = HttpContext.Current.Server.MapPath("~/Images/" + postedFile.FileName);
                                //newProduct.ImagePath = filePath;

                                var description = httpRequest.Form.Get("Description");
                                var value = httpRequest.Form.Get("Value");

                                var productModel = new Models.Produto()
                                {

                                    ImagePath = postedFile.FileName,
                                    Description = description,
                                    Value = Convert.ToDouble(value),
                                };

                                context.Produtos.Add(productModel);
                                context.SaveChanges();
                                postedFile.SaveAs(filePath);

                            }
                        }
                        

                        var message1 = string.Format("Product and Image Updated Successfully.");
                        return Request.CreateErrorResponse(HttpStatusCode.Created, message1);
                    }

                    


                    
                    //var message = string.Format("Produto cadastrado com sucesso");
                    //return Request.CreateErrorResponse(HttpStatusCode.OK, message);

                    
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
