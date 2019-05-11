using PFC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PFC.DAO;
using System.Threading.Tasks;

namespace PFC.Business
{
    public class RankBLL
    {


        #region Consulta na tabela rank  diario  
        public List<Usuario> ListarRank()
        {
            RankingDAO dao = new RankingDAO();

            List<Usuario> listarRanking = new List<Usuario>();
            listarRanking = dao.ListandoTabelaRank();
            return listarRanking;
        }
        #endregion

        #region Consulta na tabela rank semanal  
        public List<Usuario> ListarRankSemanal()
        {
            RankingDAO dao = new RankingDAO();

            List<Usuario> listarRanking = new List<Usuario>();
            listarRanking = dao.ListandoTabelaRankSemanal();
            return listarRanking;
        }
        #endregion

        #region Consulta na tabela rank mensal  
        public List<Usuario> ListarRankMensal()
        {
            RankingDAO dao = new RankingDAO();

            List<Usuario> listarRanking = new List<Usuario>();
            listarRanking = dao.ListandoTabelaRankMensal();
            return listarRanking;
        }
        #endregion

        #region Listar Usuarios Inicial
        public List<Usuario> ListarUsuariosInicial()
        {
            RankingDAO dao = new RankingDAO();

            List<Usuario> listarRanking = new List<Usuario>();
            listarRanking = dao.UsuariosInicials();
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

