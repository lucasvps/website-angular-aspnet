using AspNetWebApi.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetWebApi.Controllers
{
    public class MensagensController : ApiController
    {
        public class NovaMensagem
        {
            public long IdContato { get; set; }
            public string Descricao { get; set; }
        }

        [HttpPost]
        public void Post(NovaMensagem novaMensagem)
        {
            using (var contexto = new Contexto())
            {
                var contatoModelo = contexto.Contatos
                    .Where(x => x.Id == novaMensagem.IdContato)
                    .Single();

                var mensagemModelo = new Models.Mensagem()
                {
                    Descricao = novaMensagem.Descricao,
                    Contato = contatoModelo,
                    DataHora = DateTime.Now
                };

                contexto.Mensagens.Add(mensagemModelo);
                contexto.SaveChanges();
            }
        }
    }
}
