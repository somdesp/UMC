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
        // POST: api/TopicoAPI
        [AcceptVerbs("POST")]
        public async Task FechaTopico([FromBody]Topico topico)
        {
            TopicoBLL topicoBll = new TopicoBLL();
            if (topico.usuario.Id == User.Identity.GetUserId<int>())
            {
                if (topicoBll.FechaTopico(topico) == true)
                {
                    await emailBLL.EnviarEmail(null,topico,null);
                    //return (true);
                };
            }
           // return (false);
        }

        // POST: api/TopicoAPI
        [AcceptVerbs("POST")]
        public async void EnviaEmailTopicoFechado([FromBody]Topico topico)
        {
            TopicoBLL topicoBll = new TopicoBLL();
            await topicoBll.EnviarEmail(topico);
        }
    }
}
