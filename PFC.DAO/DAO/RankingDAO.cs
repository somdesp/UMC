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
        public async Task<List<Usuario>> ListarMédiaUsuario()
        {
            SqlCommand comando;
            List<Usuario> listarUsuarios = new List<Usuario>();

            string querSQL = String.Format(" SELECT  AVG(AV.Pontos) AS Pontos,AV.Id_Usuario AS ID, US.Nome AS Nome FROM Avaliacao AV  " +
            "INNER JOIN Usuario US ON US.Id = AV.Id_Usuario  " +
            "INNER JOIN Topico TP ON TP.Id = AV.Id_Topico  " +
            "WHERE TP.IdTopicoPai IS NOT NULL GROUP BY US.Nome, AV.Id_Usuario  " +
            "ORDER BY AVG(AV.Pontos) ");

            SqlDataReader reader;
            using (contexto = new Contexto())
            {
                comando = new SqlCommand(querSQL, contexto.forumConexao);

                reader = await contexto.ExecutaComandoComRetorno(querSQL);

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

        public async Task<List<Usuario>> ListarSomaUsuario()
        {
            SqlCommand comando;
            List<Usuario> listarUsuarios = new List<Usuario>();

            string querSQL = String.Format("SELECT  SUM(AV.Pontos) AS Pontos,AV.Id_Usuario AS ID, US.Nome AS Nome FROM Avaliacao AV  " +
            "INNER JOIN Usuario US ON US.Id = AV.Id_Usuario " +
            "INNER JOIN Topico TP ON TP.Id = AV.Id_Topico " +
            "WHERE TP.IdTopicoPai IS NULL GROUP BY US.Nome, AV.Id_Usuario " +
            "ORDER BY SUM(AV.Pontos) ASC");

            SqlDataReader reader;
            using (contexto = new Contexto())
            {
                comando = new SqlCommand(querSQL, contexto.forumConexao);

                reader = await contexto.ExecutaComandoComRetorno(querSQL);

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

        #region Verificando tabela rank diario
        public async Task<List<Usuario>> ListandoTabelaRank()
        {
            SqlCommand comando;
            List<Usuario> listarUsuarios = new List<Usuario>();
            string querySQL = $"select usu.Id'Id',usu.Nome'Nome',curso.Curso'Curso',diario.Pontos'Pontos' from  TempRankingDiario diario (NOLOCK)";
            querySQL += " INNER JOIN Usuario usu(NOLOCK) ON diario.idUsuario = usu.Id";
            querySQL += " INNER JOIN Curso curso(NOLOCK) ON diario.IdCurso = curso.Id";
            querySQL += " ORDER by diario.Pontos desc";
            SqlDataReader reader;
            using (contexto = new Contexto())
            {
                comando = new SqlCommand(querySQL, contexto.forumConexao);

                reader = await contexto.ExecutaComandoComRetorno(querySQL);

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = Convert.ToInt16(reader["Id"].ToString());
                    usuario.Nome = reader["Nome"].ToString();
                    usuario.Curso.curso = reader["Curso"].ToString();
                    usuario.avaliacao.pontos = float.Parse(reader["Pontos"].ToString());
                    listarUsuarios.Add(usuario);
                }

            }
            return listarUsuarios;


        }
        #endregion

        #region Listar tabela Semanal
        public async Task<List<Usuario>> ListandoTabelaRankSemanal()
        {
            SqlCommand comando;
            List<Usuario> listarUsuarios = new List<Usuario>();
            string querySQL = $"select usu.Id,usu.Nome'Nome',curso.Curso,semanal.Pontos from  TempRankingSemanal semanal (NOLOCK)";
            querySQL += " INNER JOIN Usuario usu(NOLOCK) ON semanal.idUsuario = usu.Id";
            querySQL += " INNER JOIN Curso curso(NOLOCK) ON semanal.IdCurso = curso.Id";
            querySQL += " ORDER by semanal.Pontos desc";
            SqlDataReader reader;
            using (contexto = new Contexto())
            {
                comando = new SqlCommand(querySQL, contexto.forumConexao);

                reader = await contexto.ExecutaComandoComRetorno(querySQL);

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = Convert.ToInt16(reader["Id"].ToString());
                    usuario.Nome = reader["Nome"].ToString();
                    usuario.Curso.curso = reader["Curso"].ToString();
                    usuario.avaliacao.pontos = float.Parse(reader["Pontos"].ToString());
                    listarUsuarios.Add(usuario);
                }

            }
            return listarUsuarios;


        }
        #endregion

        #region Listar tabela Mensal
        public async Task<List<Usuario>> ListandoTabelaRankMensal()
        {
            SqlCommand comando;
            List<Usuario> listarUsuarios = new List<Usuario>();
            string querySQL = $"select usu.Id'Id',usu.Nome'Nome',curso.Curso'Curso',mensal.Pontos'Pontos' from  TempRankingMensal mensal (NOLOCK)";
            querySQL += " INNER JOIN Usuario usu(NOLOCK) ON mensal.idUsuario = usu.Id";
            querySQL += " INNER JOIN Curso curso(NOLOCK) ON mensal.IdCurso = curso.Id";
            querySQL += " ORDER by mensal.Pontos desc";
            SqlDataReader reader;
            using (contexto = new Contexto())
            {
                comando = new SqlCommand(querySQL, contexto.forumConexao);

                reader = await contexto.ExecutaComandoComRetorno(querySQL);

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = Convert.ToInt16(reader["Id"].ToString());
                    usuario.Nome = reader["Nome"].ToString();
                    usuario.Curso.curso = reader["Curso"].ToString();
                    usuario.avaliacao.pontos = float.Parse(reader["Pontos"].ToString());
                    listarUsuarios.Add(usuario);
                }

            }
            return listarUsuarios;


        }
        #endregion

        #region Usuarios no Rank inicial
        public async Task<List<Usuario>> UsuariosInicials()
        {
            SqlCommand comando;
            List<Usuario> listarUsuarios = new List<Usuario>();
            string querySQL = $"select Top 3 ava.id_Usuario_DonoTopico 'Id', u.Nome'Nome',c.Curso'Curso',";
            querySQL += "CASE WHEN SUM(ava.Pontos) is Null or SUM(ava.Pontos)<0 then 0 ";
            querySQL += "ELSE SUM(ava.Pontos) ";
            querySQL += "End 'Pontos' ";
            querySQL += "FROM(Avaliacao ava ";
            querySQL += "INNER JOIN Usuario u ON(ava.id_Usuario_DonoTopico = u.Id) ";
            querySQL += "Left JOIN Curso c ON(u.Id_Curso = c.Id)) ";
            querySQL += "WHERE ava.Data_Avaliacao > CONVERT(date, GETDATE(), 111) ";
            querySQL += "GROUP by u.Id,u.Nome,c.Curso,ava.id_Usuario_DonoTopico ";
            querySQL += "ORDER BY Pontos desc";
            SqlDataReader reader;
            using (contexto = new Contexto())
            {
                comando = new SqlCommand(querySQL, contexto.forumConexao);

                reader = await contexto.ExecutaComandoComRetorno(querySQL);

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = Convert.ToInt16(reader["Id"].ToString());
                    usuario.Nome = reader["Nome"].ToString();
                    usuario.Curso.curso = reader["Curso"].ToString();
                    usuario.avaliacao.pontos = float.Parse(reader["Pontos"].ToString());
                    listarUsuarios.Add(usuario);
                }

            }
            return listarUsuarios;


        }
        #endregion














        #region Método chamado pelo job para executar procedure diario
        public void ExecutarRankingDiario()
        {
            SqlCommand comando;
            string executarSQL = $"INSERT INTO TempRankingDiario EXEC RankingDiario";
            using (contexto = new Contexto())
            {
                comando = new SqlCommand(executarSQL, contexto.forumConexao);
                contexto.ExecutarInsert(executarSQL);
            }

        }
        #endregion

        #region Método chamado pelo job para executar procedure semanal
        public void ExecutarRankingSemanal()
        {
            SqlCommand comando;
            string executarSQL = $"INSERT INTO TempRankingSemanal Exec RankingSemanal";
            using (contexto = new Contexto())
            {
                comando = new SqlCommand(executarSQL, contexto.forumConexao);
                contexto.ExecutarInsert(executarSQL);
            }

        }
        #endregion


        #region Método chamado pelo job para executar procedure mensal
        public void ExecutarRankingMensal()
        {
            SqlCommand comando;
            string executarSQL = $"INSERT INTO TempRankingMensal EXEC RankingMensal";
            using (contexto = new Contexto())
            {
                comando = new SqlCommand(executarSQL, contexto.forumConexao);
                contexto.ExecutarInsert(executarSQL);
            }

        }
        #endregion



    }
}
