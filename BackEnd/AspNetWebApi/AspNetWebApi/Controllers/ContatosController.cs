using AspNetWebApi.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetWebApi.Controllers
{
    public class ContatosController : ApiController
    {
        public class Contato
        {
            public long Id { get; set; }
            public string Nome { get; set; }
        }

        [HttpGet]
        public List<Contato> Get()
        {
            using (var contexto = new Contexto())
            {
                var contatosModelo = contexto.Contatos.ToList();
                var contatosProxy = new List<Contato>();

                foreach (var contatoModelo in contatosModelo)
                {
                    var contatoProxy = new Contato()
                    {
                        Id = contatoModelo.Id,
                        Nome = contatoModelo.Nome
                    };

                    contatosProxy.Add(contatoProxy);
                }

                return contatosProxy;
            }
        }

        [HttpGet]
        public Contato Get(long id)
        {
            using (var contexto = new Contexto())
            {
                var contatoModelo = contexto.Contatos
                    .Where(x => x.Id == id)
                    .Single();


                var contatoProxy = new Contato()
                {
                    Id = contatoModelo.Id,
                    Nome = contatoModelo.Nome
                };

                return contatoProxy;
            }
        }

        public class NovoContato
        {
            public string Nome { get; set; }
        }

        [HttpPost]
        public void Post(NovoContato novoContato)
        {
            using (var contexto = new Contexto())
            {
                var contatoModelo = new Models.Contato()
                {
                    Nome = novoContato.Nome
                };

                contexto.Contatos.Add(contatoModelo);
                contexto.SaveChanges();
            }
        }

        public class Mensagem
        {
            public string Descricao { get; set; }
            public DateTime DataHora { get; set; }
        }

        [HttpGet]
        [Route("api/contatos/{id}/mensagens")]
        public List<Mensagem> Mensagens(long id)
        {
            using (var contexto = new Contexto())
            {
                var mensagensProxy = new List<Mensagem>();

                var contatoModelo = contexto.Contatos
                    .Include(x => x.Mensagens)
                    .Where(x => x.Id == id)
                    .Single();

                foreach (var mensagemModelos in contatoModelo.Mensagens)
                {
                    var mensagemProxy = new Mensagem()
                    {
                        DataHora = mensagemModelos.DataHora,
                        Descricao = mensagemModelos.Descricao
                    };

                    mensagensProxy.Add(mensagemProxy);
                }

                return mensagensProxy;
            }
        }

    }
}
