using PFC.Model;
using System.Collections.Generic;
using PFC.DAO;
using System.Threading.Tasks;

namespace PFC.Business
{
    public class RankBLL
    {


        #region Consulta na tabela rank  diario  
        public async Task<List<Usuario>> ListarRank()
        {
            RankingDAO dao = new RankingDAO();

            List<Usuario> listarRanking = new List<Usuario>();
            listarRanking = await dao.ListandoTabelaRank();
            return listarRanking;
        }
        #endregion

        #region Consulta na tabela rank semanal  
        public async Task<List<Usuario>> ListarRankSemanal()
        {
            RankingDAO dao = new RankingDAO();

            List<Usuario> listarRanking = new List<Usuario>();
            listarRanking = await dao.ListandoTabelaRankSemanal();
            return listarRanking;
        }
        #endregion

        #region Consulta na tabela rank mensal  
        public async Task<List<Usuario>> ListarRankMensal()
        {
            RankingDAO dao = new RankingDAO();

            List<Usuario> listarRanking = new List<Usuario>();
            listarRanking = await dao.ListandoTabelaRankMensal();
            return listarRanking;
        }
        #endregion

        #region Listar Usuarios Inicial
        public async Task<List<Usuario>> ListarUsuariosInicial()
        {
            RankingDAO dao = new RankingDAO();

            List<Usuario> listarRanking = new List<Usuario>();
            listarRanking = await dao.UsuariosInicials();
            return listarRanking;
        }
        #endregion



        #region Executar rank diario
        public void ExecutarRankDiarioJob()
        {
            RankingDAO dao = new RankingDAO();
            dao.ExecutarRankingDiario();
        }
        #endregion

        #region Executar rank Semanal
        public void ExecutarRankSemanalJob()
        {
            RankingDAO dao = new RankingDAO();
            dao.ExecutarRankingSemanal();
        }
        #endregion

        #region Executar rank Mensal
        public void ExecutarRankMensalJob()
        {
            RankingDAO dao = new RankingDAO();
            dao.ExecutarRankingMensal();
        }
        #endregion
    }

}

