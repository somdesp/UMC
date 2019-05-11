using System;
using System.Threading.Tasks;
using PFC.DAO;
using PFC.Model;

namespace PFC.Business
{
    public class NotificacaoBLL
    {
        NotificacaoDAO notificacaoDao = new NotificacaoDAO();
        public bool AdicionaNotificação(dynamic notificacao)
        {
            return notificacaoDao.NotificaçãoAmizadeAsync(notificacao);
            
        }

        public  async Task<bool> VerificaNotificacaoAmizadeAsync(Usuario notificacao)
        {
            return await  notificacaoDao.VerificaNotificacaoAmizadeAsync(notificacao);
        }
    }
}
