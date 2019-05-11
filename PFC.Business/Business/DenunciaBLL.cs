using PFC.DAO;
using PFC.Model;
using System.Threading.Tasks;

namespace PFC.Business
{
    public class DenunciaBLL
    {
        DenunciaDAO denunciaDAO = new DenunciaDAO();
        public async Task<bool> DenunciaUsuario(Denuncia denuncia)
        {
            return await denunciaDAO.DenunciaUsuarioAsync(denuncia);
        }
    }
}
