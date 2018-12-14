using PFC.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFC.DAO
{
    public class RankingDAO
    {
        #region Listar média Estrela usuario
        private Contexto contexto;
        public List<Usuario> ListarMédiaUsuario()
        {
            SqlCommand comando;
            List<Usuario> listarUsuarios = new List<Usuario>();
            
            string querSQL = String.Format(" SELECT  AVG(AV.Pontos) AS Pontos,AV.Id_Usuario AS ID, US.Nome AS Nome FROM Avaliacao AV  "+
            "INNER JOIN Usuario US ON US.Id = AV.Id_Usuario  "+
            "INNER JOIN Topico TP ON TP.Id = AV.Id_Topico  "+
            "WHERE TP.IdTopicoPai IS NOT NULL GROUP BY US.Nome, AV.Id_Usuario  "+
            "ORDER BY AVG(AV.Pontos) ");
            
            SqlDataReader reader;
            using (contexto = new Contexto())
            {
                comando = new SqlCommand(querSQL, contexto.forumConexao);

                reader = contexto.ExecutaComandoComRetorno(querSQL);

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Nome = reader["Nome"].ToString();
                    usuario.avaliacao.idUsuario = Convert.ToInt16(reader["ID"].ToString());
                    usuario.avaliacao.pontos = Convert.ToInt16(reader["Pontos"].ToString());
                    listarUsuarios.Add(usuario);
                }

            }
            return listarUsuarios;



        }
        #endregion

        #region Listar somas curti usuario
        
        public List<Usuario> ListarSomaUsuario()
        {
            SqlCommand comando;
            List<Usuario> listarUsuarios = new List<Usuario>();
            
            string querSQL = String.Format("SELECT  SUM(AV.Pontos) AS Pontos,AV.Id_Usuario AS ID, US.Nome AS Nome FROM Avaliacao AV  "+
            "INNER JOIN Usuario US ON US.Id = AV.Id_Usuario "+
            "INNER JOIN Topico TP ON TP.Id = AV.Id_Topico "+
            "WHERE TP.IdTopicoPai IS NULL GROUP BY US.Nome, AV.Id_Usuario "+
            "ORDER BY SUM(AV.Pontos) ASC");
            
            SqlDataReader reader;
            using (contexto = new Contexto())
            {
                comando = new SqlCommand(querSQL, contexto.forumConexao);

                reader = contexto.ExecutaComandoComRetorno(querSQL);

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Nome = reader["Nome"].ToString();
                    usuario.avaliacao.idUsuario = Convert.ToInt16(reader["ID"].ToString());
                    usuario.avaliacao.pontos = Convert.ToInt16(reader["Pontos"].ToString());
                    listarUsuarios.Add(usuario);
                }

            }
            return listarUsuarios;



        }
        #endregion


    }
}
