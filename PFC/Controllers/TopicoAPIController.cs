using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using PFC.Business.Business;
using PFC.Model;

namespace PFC.Controllers
{
    public class TopicoAPIController : ApiController
    {
        // POST: api/TopicoAPI
        [AcceptVerbs("POST")]
        public bool FechaTopico([FromBody]Topico topico)
        {
            TopicoBLL topicoBll = new TopicoBLL();
            if (topico.usuario.Id == User.Identity.GetUserId<int>())
            {
                if (topicoBll.FechaTopico(topico) == true)
                {
                    topicoBll.EnviarEmail(topico);
                    return (true);
                };
            }
            return (false);
        }

        // POST: api/TopicoAPI
        [AcceptVerbs("POST")]
        public void EnviaEmailTopicoFechado([FromBody]Topico topico)
        {
            TopicoBLL topicoBll = new TopicoBLL();
            topicoBll.EnviarEmail(topico);
        }
    }
}
