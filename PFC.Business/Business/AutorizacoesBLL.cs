using System.Collections.Generic;
using System.Threading.Tasks;
using PFC.DAO;
using PFC.Model;

namespace PFC.Business
{
    public class AutorizacoesBLL
    {
        AutorizacoesDAO permisaoDao = new AutorizacoesDAO();

        public async Task<Autorizacoes> ReturnAutPorID(Autorizacoes auth)
        {
            return await permisaoDao.ReturnAutPorID(auth);
        }

        public async Task<List<Autorizacoes>> ListarAutorizacoes()
        {
            return await permisaoDao.ListarAutorizacoes();
        }
        
    }
}
