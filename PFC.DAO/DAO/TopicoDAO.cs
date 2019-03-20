using PFC.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace PFC.DAO
{
    public class TopicoDAO
    {
        private Contexto contexto;


        #region Adicionar Topico
        public bool AdicionarTopico(Topico topico)
        {

            var strQuery = "";
            strQuery += "INSERT INTO Topico (Titulo,Id_Tema,Id_Usuario,Descricao,DataCriacao,DataUpdate,Id_Status) ";
            strQuery += string.Format("VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                topico.Titulo, topico.Tema.Id, topico.usuario.Id, topico.Descricao, DateTime.Now, DateTime.Now, topico.Status.Id);

            using (contexto = new Contexto())
            {
                return contexto.ExecutarInsert(strQuery);
            }

        }
        #endregion

        #region Listar Topicos Pai
        public List<Topico> ListarTopico()
        {
            var topico = new List<Topico>();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                var strQuery = " SELECT * FROM Topico WHERE IdTopicoPai IS NULL ORDER BY DataUpdate DESC ";
                reader = contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    Topico readerTopico = new Topico();
                    readerTopico.Id = Convert.ToInt32(reader["Id"].ToString());
                    readerTopico.Titulo = reader["Titulo"].ToString();
                    readerTopico.DataCria = Convert.ToDateTime(reader["DataCriacao"].ToString());
                    readerTopico.Descricao = reader["Descricao"].ToString();
                    readerTopico.usuario.Id = Convert.ToInt32(reader["Id_Usuario"].ToString());
                    readerTopico.Tema.Id = Convert.ToInt32(reader["Id_Tema"].ToString());
                    topico.Add(readerTopico);
                }
            }
            reader.Close();
            return topico;
        }
        #endregion

        #region Listar Topicos Filhos
        public List<Topico> ListarTopicoFilho(int idTopico)
        {
            List<Topico> topico = new List<Topico>();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                var strQuery = string.Format(" SELECT * FROM Topico WHERE IdTopicoPai ={0}",idTopico);
                reader = contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    Topico readerTopico = new Topico();
                    readerTopico.Id = Convert.ToInt32(reader["Id"].ToString());
                    readerTopico.Titulo = reader["Titulo"].ToString();
                    readerTopico.DataCria = Convert.ToDateTime(reader["DataCriacao"].ToString());
                    readerTopico.Descricao = reader["Descricao"].ToString();
                    readerTopico.usuario.Id = Convert.ToInt32(reader["Id_Usuario"].ToString());
                    readerTopico.Tema.Id = Convert.ToInt32(reader["Id_Tema"].ToString());
                    topico.Add(readerTopico);
                }
            }
            reader.Close();
            return topico;
        }
        #endregion

        #region Detalhe Topico
        public Topico DetalheTopico(Topico topico)
        {
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                var strQuery = string.Format(" SELECT * FROM Topico WHERE Id ='{0}' ", topico.Id);
                reader = contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    Topico readerTopico = new Topico();
                    readerTopico.Id = Convert.ToInt32(reader["Id"].ToString());
                    readerTopico.Titulo = reader["Titulo"].ToString();
                    readerTopico.DataCria = Convert.ToDateTime(reader["DataCriacao"].ToString());
                    readerTopico.Descricao = reader["Descricao"].ToString();
                    readerTopico.usuario.Id = Convert.ToInt32(reader["Id_Usuario"].ToString());
                    readerTopico.Tema.Id = Convert.ToInt32(reader["Id_Tema"].ToString());
                    readerTopico.Status.Id= Convert.ToInt32(reader["Id_Status"].ToString());
                    topico = (readerTopico);

                }
            }
            reader.Close();
            return topico;
        }
        #endregion

        #region Adicionar Posts (Respostas)
        public int AdicionarPosts(Topico post)
        {
            int retorno = 0;
            SqlDataReader reader;
            var strQuery = "";
            strQuery += "INSERT INTO Topico(Titulo,Descricao,Id_Tema,Id_Usuario ,IdTopicoPai,DataCriacao) ";
            strQuery += string.Format("VALUES('{0}','{1}','{2}','{3}','{4}','{5}'); SELECT SCOPE_IDENTITY()AS retorno;",
                post.Titulo,post.TopicoFilho.Descricao,post.Tema.Id, post.TopicoFilho.usuario.Id, post.Id,DateTime.Now);

            using (contexto = new Contexto())
            {
                reader = contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    retorno = Convert.ToInt32(reader["retorno"].ToString());

                }
            }

            return retorno;
        }
        #endregion

        //#region Ordernar Por ultimo Post
        //public List<Topico> ListarTopicoUtimPost()
        //{
        //    var topico = new List<Topico>();
        //    SqlDataReader reader;

        //    using (contexto = new Contexto())
        //    {
        //        var strQuery = " SELECT * FROM Topico WHERE IdTopicoPai IS NOT NULL ORDER BY DataCriacao DESC ";
        //        reader = contexto.ExecutaComandoComRetorno(strQuery);

        //        while (reader.Read())
        //        {
        //            Topico readerTopico = new Topico();
        //            readerTopico.Id = Convert.ToInt32(reader["Id"].ToString());
        //            readerTopico.Titulo = reader["Titulo"].ToString();
        //            readerTopico.DataCria = Convert.ToDateTime(reader["DataCriacao"].ToString());
        //            readerTopico.Descricao = reader["Descricao"].ToString();
        //            readerTopico.usuario.Id = Convert.ToInt32(reader["Id_Usuario"].ToString());
        //            readerTopico.Tema.Id = Convert.ToInt32(reader["Id_Tema"].ToString());
        //            readerTopico.IdTopicoPai = Convert.ToInt32(reader["IdTopicoPai"].ToString());
        //            topico.Add(readerTopico);
        //        }
        //    }
        //    reader.Close();
        //    return topico;
        //}
        //#endregion

        #region ValTopico se Existe

        public Topico ValTopico(Topico topico)
        {
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                var strQuery = string.Format(" SELECT * FROM Topico WHERE Id ='{0}' AND IdTopicoPai IS NULL", topico.Id);
                reader = contexto.ExecutaComandoComRetorno(strQuery);
                Topico readerTopico = new Topico();
                while (reader.Read())
                {
                    readerTopico.Id = Convert.ToInt32(reader["Id"].ToString());
                    readerTopico.Titulo = reader["Titulo"].ToString();
                    readerTopico.DataCria = Convert.ToDateTime(reader["DataCriacao"].ToString());
                    readerTopico.Descricao = reader["Descricao"].ToString();
                    readerTopico.usuario.Id = Convert.ToInt32(reader["Id_Usuario"].ToString());
                    readerTopico.Tema.Id = Convert.ToInt32(reader["Id_Tema"].ToString());
                }
                topico = (readerTopico);
            }
            reader.Close();
            return topico;

        }

        #endregion

        #region Update Data Topico

        public bool UpdateDataTopico(Topico post)
        {
            var strQuery = "";
            strQuery += string.Format("UPDATE Topico SET DataUpdate = '{0}' WHERE Id='{1}'", DateTime.Now, post.Id);
            using (contexto = new Contexto())
            {
                return contexto.ExecutarInsert(strQuery);
            }
        }


        #endregion

        #region Listar Topicos Pai conforme a pesquisa do usuario
        public List<Topico> ListarTopicoPesquisa(string pesquisa)
        {
            var topico = new List<Topico>();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                var strQuery =String.Format(" SELECT * FROM Topico WHERE IdTopicoPai IS NULL and Titulo Like '%{0}%' ",pesquisa);
                reader = contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    Topico readerTopico = new Topico();
                    readerTopico.Id = Convert.ToInt32(reader["Id"].ToString());
                    readerTopico.Titulo = reader["Titulo"].ToString();
                    readerTopico.DataCria = Convert.ToDateTime(reader["DataCriacao"].ToString());
                    readerTopico.Descricao = reader["Descricao"].ToString();
                    readerTopico.usuario.Id = Convert.ToInt32(reader["Id_Usuario"].ToString());
                    readerTopico.Tema.Id = Convert.ToInt32(reader["Id_Tema"].ToString());
                    topico.Add(readerTopico);
                }
            }
            reader.Close();
            return topico;
        }
        #endregion

        #region FechaTopico

        public bool FechaTopico(Topico topico)
        {
            var strQuery = "";
            strQuery += string.Format("UPDATE Topico SET DataUpdate = GETDATE(),Id_Status= 2 WHERE Id='{0}'",  topico.Id);
            using (contexto = new Contexto())
            {
                return contexto.ExecutarInsert(strQuery);
            }
        }

        #endregion
    }
}
