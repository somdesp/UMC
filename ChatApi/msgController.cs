using System.Web.Http;
using System.Web.Http.Cors;


namespace ChatApi
{
    public class msgController : ApiController
    {
        MessageRepo msgRepo = new MessageRepo();

        [HttpGet]
        [EnableCors(origins: "http://localhost:52005", headers: "*", methods: "*")]
        public void registerUser(string username, string connectionID)
        {
            msgRepo.registerUser(username, "", connectionID);
        }

        [HttpGet]
        [EnableCors(origins: "http://localhost:52005", headers: "*", methods: "*")]
        public void sendMessage(string userid, string msg)
        {
            msgRepo.addMessage(userid, msg);
        }
    }
}