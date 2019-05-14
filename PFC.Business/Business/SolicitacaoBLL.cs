using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PFC.DAO;
using PFC.Model;

namespace PFC.Business
{
    public class SolicitacaoBLL
    {
        SolicitacaoDAO amizadeDao = new SolicitacaoDAO();
        NotificacaoBLL notificacaoBLL = new NotificacaoBLL();

        public async Task<bool> solicitacaoAmizade(Usuario usuario, Usuario usuarioSolicitado)
        {
            try
            {
                int id_amizade = await amizadeDao.solicitacaoAmizade(usuario, usuarioSolicitado);
                if (id_amizade > 0)
                {
                    await notificacaoBLL.AdicionaNotificação(id_amizade);
                    return true;
                }
                return false;

            }
            catch (Exception)
            {

                throw;
            }



        }

        public async Task<bool> ValidaAmizade(Usuario usuario, Usuario usuarioSolicitado)
        {
            return await amizadeDao.ValidaAmizade(usuario, usuarioSolicitado);
        }


        public async Task<bool> AceitaAmizade(Usuario usuario, Usuario usuarioSolicitado)
        {
            return await amizadeDao.AceitaAmizadeAsync(usuario, usuarioSolicitado);
        }

        public async Task<bool> CancelaAmizade(Usuario usuario, Usuario usuarioSolicitado)
        {
            return await amizadeDao.CancelaAmizade(usuario, usuarioSolicitado);
        }

        public async Task<List<Usuario>> ListaAmizade(int Id_Usuario)
        {
            Usuario usuario = new Usuario();
            usuario.Id = Id_Usuario;
            return await amizadeDao.ListaAmizade(usuario);
        }


    }
}
