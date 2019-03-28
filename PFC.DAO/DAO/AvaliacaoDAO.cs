using PFC.Model;
using System;
using System.Data.SqlClient;

namespace PFC.DAO
{
    public class AvaliacaoDAO
    {
        private Contexto contexto;


        #region inserir e voltar valor da media dos pontos avaliados
        public Avaliacao InserirPonto(Avaliacao avaliar, double pontosUsuario)
        {


            Avaliacao resultAvaliacao = new Avaliacao();
            SqlCommand comando;
            using (contexto = new Contexto())
            {

                string strQuery = String.Format("insert into Avaliacao (Id_Topico,Id_Usuario,Data_Avaliacao,Pontos,id_Usuario_DonoTopico) Values({0},{1},(Select GETDATE()),{2},(Select Id_Usuario from Topico where Id = {0})) select Pontos,(select avg(Pontos) from Avaliacao where Id_Topico = {0}) as media from Avaliacao where Id_Topico = {0} and Id_Usuario = {1} ", avaliar.idTopico, avaliar.idUsuario, pontosUsuario);

                bool result = false;
                SqlDataReader reader;
                using (contexto = new Contexto())
                {
                    comando = new SqlCommand(strQuery, contexto.forumConexao);

                    reader = contexto.ExecutaComandoComRetorno(strQuery);

                    while (reader.Read())
                    {
                        avaliar.pontos = Convert.ToInt16(reader["Pontos"].ToString());
                        avaliar.mediaPontos = Convert.ToInt16(reader["media"].ToString());
                    }

                }
                result = AvaliacaoPontos(avaliar.idTopico);
                if (result)
                {
                    return avaliar;
                }
                else
                {
                    return null;
                }


            }


        }
        #endregion

        #region inserir e voltar valor da Like/Deslike dos pontos avaliados
        public Avaliacao InserirPontoLikeDeslike(Avaliacao avaliar, double pontosUsuario)
        {


            Avaliacao resultAvaliacao = new Avaliacao();
            SqlCommand comando;
            using (contexto = new Contexto())
            {

                string strQuery = String.Format("insert into Avaliacao (Id_Topico,Id_Usuario,Data_Avaliacao,Pontos) Values({0},{1},(Select GETDATE()),{2}) select Pontos,(select count(Pontos) from Avaliacao where Id_Topico = {0} and Pontos = 1) as contarLike,(select count(Pontos) from Avaliacao where Id_Topico = {0} and Pontos = -1) as contarDeslike from Avaliacao where Id_Topico = {0} and Id_Usuario = {1}", avaliar.idTopico, avaliar.idUsuario, pontosUsuario);


                SqlDataReader reader;
                using (contexto = new Contexto())
                {
                    comando = new SqlCommand(strQuery, contexto.forumConexao);

                    reader = contexto.ExecutaComandoComRetorno(strQuery);

                    while (reader.Read())
                    {
                        avaliar.pontos = Convert.ToInt16(reader["pontos"].ToString());
                        avaliar.contarLike = Convert.ToInt16(reader["contarLike"].ToString());
                        avaliar.contarDeslike = Convert.ToInt16(reader["contarDeslike"].ToString());
                    }

                }
                return avaliar;

            }


        }
        #endregion

        #region Deletar registro avaliacao
        public bool DeletarAvaliacao(Avaliacao avaliacao)
        {
            var strQuery = String.Format("update Avaliacao set Pontos = 0 where Id_Topico = {0} and Id_Usuario = {1}", avaliacao.idTopico, avaliacao.idUsuario);


            using (contexto = new Contexto())
            {
                return contexto.ExecutarInsert(strQuery);
            }
        }
        #endregion

        #region Consulta Pontos por tópico
        public float consultaAvaliarpontos(Avaliacao avaliar, int idUsuario)
        {

            SqlCommand comando;

            string querSQL = String.Format("select Pontos from Avaliacao where Id_Topico = {0} and Id_Usuario = {1}", avaliar.idTopico, idUsuario);

            SqlDataReader reader;
            using (contexto = new Contexto())
            {
                comando = new SqlCommand(querSQL, contexto.forumConexao);

                reader = contexto.ExecutaComandoComRetorno(querSQL);
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        avaliar.pontos = float.Parse(reader["Pontos"].ToString());

                    }
                }
                else
                {
                    avaliar.pontos = 0;
                }


            }
            return avaliar.pontos;
        }


        #endregion

        #region Consulta Média por tópico
        public float consultaMediaAvaliacao(Avaliacao avaliar)
        {

            SqlCommand comando;

            string querSQL = String.Format("select avg(pontos) as media from Avaliacao where Id_Topico = {0}", avaliar.idTopico);

            SqlDataReader reader;
            using (contexto = new Contexto())
            {
                comando = new SqlCommand(querSQL, contexto.forumConexao);

                reader = contexto.ExecutaComandoComRetorno(querSQL);

                while (reader.Read())
                {
                    string media = reader["media"].ToString();
                    if (media == string.Empty)
                    {

                    }
                    else
                    {
                        avaliar.mediaPontos = float.Parse(reader["media"].ToString());
                    }


                }

            }
            return avaliar.mediaPontos;
        }


        #endregion

        #region Atualização da nota 
        public Avaliacao AtualizarPonto(Avaliacao avaliar, float pontosUsuario)
        {


            Avaliacao resultAvaliacao = new Avaliacao();
            SqlCommand comando;
            using (contexto = new Contexto())
            {
               
                
                string strQuery = String.Format("update Avaliacao set Pontos = {2},Data_Avaliacao = (select GETDATE()) where Id_Topico = {0} and Id_Usuario = {1} select Pontos,(select avg(Pontos) from Avaliacao where Id_Topico = {0}) as media from Avaliacao where Id_Topico = {0} and Id_Usuario = {1} ", avaliar.idTopico, avaliar.idUsuario, pontosUsuario.ToString().Replace(',','.'));

                bool result = false;
                SqlDataReader reader;
                using (contexto = new Contexto())
                {
                    comando = new SqlCommand(strQuery, contexto.forumConexao);

                    reader = contexto.ExecutaComandoComRetorno(strQuery);

                    while (reader.Read())
                    {
                        avaliar.pontos = float.Parse(reader["pontos"].ToString());
                        avaliar.mediaPontos = float.Parse(reader["media"].ToString());
                    }

                }
                result = AvaliacaoPontos(avaliar.idTopico);
                if (result)
                {
                    return avaliar;
                }
                else
                {
                    return null;
                }

            }


        }
        #endregion

        #region Atualização da nota 
        public Avaliacao AtualizarPontoLikeDeslike(Avaliacao avaliar, double pontosUsuario)
        {


            Avaliacao resultAvaliacao = new Avaliacao();
            SqlCommand comando;
            using (contexto = new Contexto())
            {

                string strQuery = String.Format("update Avaliacao set Pontos = {2} where Id_Topico = {0} and Id_Usuario = {1} select Pontos,(select count(Pontos) from Avaliacao where Id_Topico = {0} and Pontos = 1) as contarLike,(select count(Pontos) from Avaliacao where Id_Topico = {0} and Pontos = -1) as contarDeslike from Avaliacao where Id_Topico = {0} and Id_Usuario = {1} ", avaliar.idTopico, avaliar.idUsuario, pontosUsuario);


                SqlDataReader reader;
                using (contexto = new Contexto())
                {
                    comando = new SqlCommand(strQuery, contexto.forumConexao);

                    reader = contexto.ExecutaComandoComRetorno(strQuery);

                    while (reader.Read())
                    {
                        avaliar.pontos = float.Parse(reader["pontos"].ToString());
                        avaliar.contarLike = Convert.ToInt16(reader["contarLike"].ToString());
                        avaliar.contarDeslike = Convert.ToInt16(reader["contarDeslike"].ToString());
                    }

                }
                return avaliar;
            }


        }
        #endregion

        #region Consultar pelo Id da Avaliacao

        public int consultaAvaliacaoID(Avaliacao avaliar, int idUsuario)
        {

            SqlCommand comando;

            string querSQL = String.Format("select Id from Avaliacao where Id_Topico = {0} and Id_Usuario = {1}", avaliar.idTopico, idUsuario);

            SqlDataReader reader;
            using (contexto = new Contexto())
            {
                comando = new SqlCommand(querSQL, contexto.forumConexao);

                reader = contexto.ExecutaComandoComRetorno(querSQL);
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        avaliar.idAvaliacao = Convert.ToInt16(reader["Id"].ToString());

                    }
                }
                else
                {
                    avaliar.idAvaliacao = 0;
                }


            }
            return avaliar.idAvaliacao;
        }


        #endregion

        #region consultar pontos curtir like e deslike
        public int consultaLikeDeslike(Topico topico, int idUsuarioLogado)
        {

            SqlCommand comando;

            string querSQL = String.Format("select Pontos from Avaliacao where Id_Topico = {0} and Id_Usuario = {1}", topico.Id, idUsuarioLogado);
            int pontos = 0;
            SqlDataReader reader;
            using (contexto = new Contexto())
            {
                comando = new SqlCommand(querSQL, contexto.forumConexao);

                reader = contexto.ExecutaComandoComRetorno(querSQL);

                while (reader.Read())
                {

                    pontos = Convert.ToInt16(reader["Pontos"].ToString());

                }

            }
            return pontos;
        }

        #endregion

        #region contar pontos like
        public int consultaLike(Topico topico, int idUsuarioLogado)
        {

            SqlCommand comando;

            string querSQL = String.Format("select count(Pontos) as contarLike from Avaliacao where Id_Topico = {0} and Pontos = 1", topico.Id, idUsuarioLogado);
            int pontos = 0;
            SqlDataReader reader;
            using (contexto = new Contexto())
            {
                comando = new SqlCommand(querSQL, contexto.forumConexao);

                reader = contexto.ExecutaComandoComRetorno(querSQL);

                while (reader.Read())
                {

                    pontos = Convert.ToInt16(reader["contarLike"].ToString());

                }

            }
            return pontos;
        }

        #endregion

        #region contar pontos Deslike
        public int consultaDeslike(Topico topico, int idUsuarioLogado)
        {

            SqlCommand comando;

            string querSQL = String.Format("select count(Pontos) as Deslike from Avaliacao where Id_Topico = {0} and Pontos = -1", topico.Id, idUsuarioLogado);
            int pontos = 0;
            SqlDataReader reader;
            using (contexto = new Contexto())
            {
                comando = new SqlCommand(querSQL, contexto.forumConexao);

                reader = contexto.ExecutaComandoComRetorno(querSQL);

                while (reader.Read())
                {

                    pontos = Convert.ToInt16(reader["Deslike"].ToString());

                }

            }
            return pontos;
        }

        #endregion

        #region Avaliar pontos atualizando 
        public bool AvaliacaoPontos(int idTopico)
        {
            string strQuery = $"update Usuario set Pontos" +
                $" = (select SUM(Pontos) from Avaliacao where id_Usuario_DonoTopico=(select Id_Usuario from Topico where id={idTopico})) " +
                $"where id = (select Id_Usuario from Topico where id={idTopico}) ";
            SqlCommand comando;
            bool result = false;
            using (contexto = new Contexto())
            {
                comando = new SqlCommand(strQuery, contexto.forumConexao);

                 result= contexto.ExecutarInsert(strQuery);


            }

            return result;



        }
        #endregion
    }
}
