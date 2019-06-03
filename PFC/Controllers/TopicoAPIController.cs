using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using PFC.Business;
using PFC.Model;

namespace PFC.Controllers
{
    [Authorize]
    public class TopicoAPIController : ApiController
    {
        EmailBLL emailBLL = new EmailBLL();
        TopicoBLL topicoBll = new TopicoBLL();

        // POST: api/TopicoAPI
        [AcceptVerbs("POST")]
        public async Task<bool> FechaTopico([FromBody]Topico topico)
        {
            if (topico.usuario.Id == User.Identity.GetUserId<int>())
            {
                if (topicoBll.FechaTopico(topico) == true)
                {
                    await emailBLL.EnviarEmail(null, topico, null);
                    return true;
                };
            }
            return false;
        }

        // POST: api/TopicoAPI
        [AcceptVerbs("POST")]
        public async void EnviaEmailTopicoFechado([FromBody]Topico topico)
        {
            TopicoBLL topicoBll = new TopicoBLL();
            await topicoBll.EnviarEmail(topico);
        }


        [AcceptVerbs("POST")]
        [Authorize(Roles = "Admin,Master")]
        public async Task<List<Topico>> GetTopicosPesquisa([FromBody]dynamic topico)
        {
            try
            {
                return await topicoBll.GetTopicosPesquisa(topico);
            }
            catch (Exception)
            {
                throw;
            }

        }



        [AcceptVerbs("POST")]
        [Authorize(Roles = "Admin,Master")]
        public async Task<List<StatusTopico>> GetStatusTopico()
        {
            try
            {
                return await topicoBll.GetStatusTopico();
            }
            catch (Exception)
            {
                throw;
            }

        }

        [AcceptVerbs("POST")]
        [Authorize(Roles = "Admin,Master")]
        public async Task<bool> AlterarTopico([FromBody]Topico topico)
        {
            try
            {
                return await topicoBll.AlterarTopico(topico);
            }
            catch (Exception)
            {
                throw;
            }

        }

        
    }
}
