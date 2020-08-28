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
        public class Cliente
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }


        }

        // Retorna todos os clientes
        [HttpGet]
        public List<Cliente> Get()
        {
            using (var context = new Contexto())
            {
                var clientsModel = context.Clientes.ToList();
                var clientsList = new List<Cliente>();

                foreach(var clientModel in clientsModel)
                {
                    var client = new Cliente()
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
        public Cliente Get(long id)
        {
            using (var context = new Contexto())
            {
                var clientModel = context.Clientes
                    .Where(x => x.Id == id)
                    .Single();


                var client = new Cliente()
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

                        var isEmailAlreadyExists = context.Clientes.Any(x => x.Email == newClient.Email);
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


                            var clientModel = new Models.Cliente()
                            {
                                Name = newClient.Name,
                                Email = newClient.Email,
                            };

                            context.Clientes.Add(clientModel);
                            context.SaveChanges();
                            var message = string.Format("Sucess");
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

        public class Pedido
        {
            public long Id { get; set; }

            public long Number { get; set; }
            public double FinalCost { get; set; }

            public DateTime Date { get; set; }


        }

        [HttpGet]
        [Route("api/clients/{id}/orders")]
        public List<Pedido> Pedidos(long id)
        {
            using(var context = new Contexto())
            {
                var listOrders = new List<Pedido>();

                var clientModel = context.Clientes.Include(x => x.Orders).Where(x => x.Id == id).Single();

                foreach(var pedidoModel in clientModel.Orders)
                {
                    var pedidoProxy = new Pedido()
                    {
                        Number = pedidoModel.Number,
                        FinalCost = pedidoModel.FinalCost,
                        
                        Date = pedidoModel.Date,
                        Id = pedidoModel.Id
                    };

                    listOrders.Add(pedidoProxy);

                }

                return listOrders;
            }
        }

        
    }
}
