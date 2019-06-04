using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PFC.DAO;
using PFC.Model;

namespace PFC.Business
{
    public class NotificacaoBLL
    {
        NotificacaoDAO notificacaoDao = new NotificacaoDAO();
        public async Task<bool> AdicionaNotificação(dynamic notificacao)
        {
            return await notificacaoDao.NotificaçãoAmizadeAsync(notificacao);
            
        }

        public  async Task<bool> VerificaNotificacaoAmizadeAsync(Usuario notificacao)
        {
            return await  notificacaoDao.VerificaNotificacaoAmizadeAsync(notificacao);
        }

        public async Task<bool> VerificaNotificacaoDenunciaAsync(Usuario notificacao)
        {
            return await notificacaoDao.VerificaNotificacaoDenunciaAsync(notificacao);
        }

        public async Task<List<Amizade>> NotificacaoAmizade(Usuario usuario)
        {

            return await notificacaoDao.NotificacaoAmizade(usuario);
        }

        public async Task<List<Denuncia>> NotificacaoDenunciaAsync(Usuario usuario)
        {

            return await notificacaoDao.NotificacaoDenunciaAsync(usuario);
        }


        

    }
}
