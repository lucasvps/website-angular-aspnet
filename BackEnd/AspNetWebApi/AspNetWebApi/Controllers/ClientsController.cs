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
        public void Post(NewClient newClient)
        {
        
            using (var context = new Contexto())
            {
                var clientModel = new Models.Cliente()
                {
                    Name = newClient.Name,
                    Email = newClient.Email,
                };

                context.Clientes.Add(clientModel);
                context.SaveChanges();
            }
        }

        public class Pedido
        {
            public long Id { get; set; }

            public long Number { get; set; }
            public double FinalCost { get; set; }

            public string Date { get; set; }


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
