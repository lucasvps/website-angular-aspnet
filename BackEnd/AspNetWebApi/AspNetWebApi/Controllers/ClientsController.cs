using AspNetWebApi.Context;
using AspNetWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Services.Description;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Text.RegularExpressions;
using System.Globalization;

namespace AspNetWebApi.Controllers
{
    public class ClientsController : ApiController
    {

        // Retorna todos os clientes
        [HttpGet]
        public List<Client> Get()
        {
            using (var context = new Contexto())
            {
                var clientsModel = context.Clients.ToList();
                var clientsList = new List<Client>();

                foreach(var clientModel in clientsModel)
                {
                    var client = new Client()
                    {
                        Id = clientModel.Id,
                        Name = clientModel.Name,
                        Email = clientModel.Email,
                    };

                    clientsList.Add(client);
                }

                return clientsList;
            }
        }

        //Retorna cliente especifico
        [HttpGet]
        public Client Get(long id)
        {
            using (var context = new Contexto())
            {
                var clientModel = context.Clients
                    .Where(x => x.Id == id)
                    .Single();


                var client = new Client()
                {
                    Id = clientModel.Id,
                    Name = clientModel.Name,
                    Email = clientModel.Email,

                };

                return client;
            }
        }

        // Cadastrar novo cliente

        public class NewClient
        {
            public string Name { get; set; }
            public string Email { get; set; }

        }

        [HttpPost]
        public HttpResponseMessage Post(NewClient newClient)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    using (var context = new Contexto())
                    {

                        var isEmailAlreadyExists = context.Clients.Any(x => x.Email == newClient.Email);
                        if (isEmailAlreadyExists)
                        {
                            var message = string.Format("Este email já esta sendo utilizado");
                            return Request.CreateErrorResponse(HttpStatusCode.Conflict, message);
                        }

                        else
                        {

                            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                            Match match = regex.Match(newClient.Email);
                            if (!match.Success)
                            {
                                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email inválido!");
                            }


                            var clientModel = new Models.Client()
                            {
                                Name = newClient.Name,
                                Email = newClient.Email,
                            };

                            context.Clients.Add(clientModel);
                            context.SaveChanges();
                            var message = string.Format("Cliente adicionado com sucesso!");
                            return Request.CreateErrorResponse(HttpStatusCode.OK, message);
                        }



                    }
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

       

        [HttpGet]
        [Route("api/clients/{id}/orders")]
        public List<Order> Pedidos(long id)
        {
            using(var context = new Contexto())
            {
                var listOrders = new List<Order>();

                var clientModel = context.Clients.Include(x => x.Orders).Where(x => x.Id == id).Single();

                foreach(var orderModel in clientModel.Orders)
                {
                    List<Product> ProductsList = new List<Product>();
                    var orders = context.Orders.Where(order => order.Id == orderModel.Id).SelectMany(products => products.Products).ToList();

                    foreach (var products in orders)
                    {
                        ProductsList.Add(new Product(products.Description, products.Value, products.Image));
                    }

                    var pedidoProxy = new Order()
                    {
                        Number = orderModel.Number,
                        FinalCost = orderModel.FinalCost,
                        Date = DateTime.Now,
                        Id = orderModel.Id
                    };

                    listOrders.Add(new Order(orderModel.Id, orderModel.Discount, orderModel.Cost, orderModel.FinalCost, orderModel.Number, ProductsList, new Client(clientModel.Id, clientModel.Name, clientModel.Email), orderModel.Date));

                }

                return listOrders;
            }
        }

        
    }
}
