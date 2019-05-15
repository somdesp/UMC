using PFC.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

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
        public async Task<List<Topico>> ListarTopico()
        {
            var topico = new List<Topico>();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                var strQuery = " SELECT toc.Id ,toc.Titulo,toc.Id_Tema,toc.Id_Usuario,toc.IdTopicoPai,"
                    + "toc.Id_Status,toc.Descricao,toc.DataCriacao,usu.Id AS IdUsuario,usu.Nome,usu.Login,usu.Email, usu.Senha, usu.RGM, "
                    + "usu.DataNasci, usu.DataCad, usu.Id_Curso ,usu.Id_Semestre,usu.Id_Genero,  usu.Id_Arquivo,usu.Id_Permissoes AS IdTopico FROM Topico toc "
                    + "INNER JOIN Usuario usu ON usu.Id = toc.Id_Usuario WHERE IdTopicoPai IS NULL ORDER BY DataUpdate DESC ";
                reader = await contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    Topico readerTopico = new Topico();
                    readerTopico.Id = Convert.ToInt32(reader["Id"].ToString());
                    readerTopico.Titulo = reader["Titulo"].ToString();
                    readerTopico.DataCria = Convert.ToDateTime(reader["DataCriacao"].ToString());
                    readerTopico.Descricao = reader["Descricao"].ToString();
                    readerTopico.Tema.Id = Convert.ToInt32(reader["Id_Tema"].ToString());
                    //usuario
                    readerTopico.usuario.Id = Convert.ToInt32(reader["IdUsuario"].ToString());
                    readerTopico.usuario.Nome = reader["Nome"].ToString();
                    readerTopico.usuario.Login = reader["Login"].ToString();
                    readerTopico.usuario.Email = reader["Email"].ToString();
                    readerTopico.usuario.RGM = reader["RGM"].ToString();
                    readerTopico.usuario.DataNasci = Convert.ToDateTime(reader["DataNasci"].ToString());
                    readerTopico.usuario.DataNasci = Convert.ToDateTime(reader["DataCad"].ToString());
                    readerTopico.usuario.Curso.Id = Convert.ToInt32(reader["Id_Curso"].ToString());
                    readerTopico.usuario.Semestre.Id = Convert.ToInt32(reader["Id_Semestre"].ToString());
                    readerTopico.usuario.Sexo.Id = Convert.ToInt32(reader["Id_Genero"].ToString());
                    readerTopico.usuario.UploadArquivo.Id = Convert.ToInt32(reader["Id_Arquivo"].ToString());
                                                                             
                    topico.Add(readerTopico);
                }
            }
            reader.Close();
            return topico;
        }
        #endregion

        #region Listar Topicos Filhos
        public async Task<List<Topico>> ListarTopicoFilho(int idTopico)
        {
            List<Topico> topico = new List<Topico>();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                var strQuery = string.Format(" SELECT * FROM Topico WHERE IdTopicoPai ={0}", idTopico);
                reader = await contexto.ExecutaComandoComRetorno(strQuery);

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
        public async Task<Topico> DetalheTopico(Topico topico)
        {
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                var strQuery = string.Format(" SELECT * FROM Topico WHERE Id ='{0}' ", topico.Id);
                reader = await contexto.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    Topico readerTopico = new Topico();
                    readerTopico.Id = Convert.ToInt32(reader["Id"].ToString());
                    readerTopico.Titulo = reader["Titulo"].ToString();
                    readerTopico.DataCria = Convert.ToDateTime(reader["DataCriacao"].ToString());
                    readerTopico.Descricao = reader["Descricao"].ToString();
                    readerTopico.usuario.Id = Convert.ToInt32(reader["Id_Usuario"].ToString());
                    readerTopico.Tema.Id = Convert.ToInt32(reader["Id_Tema"].ToString());
                    readerTopico.Status.Id = Convert.ToInt32(reader["Id_Status"].ToString());
                    topico = (readerTopico);

                }
            }
            reader.Close();
            return topico;
        }
        #endregion

        #region Adicionar Posts (Respostas)
        public bool AdicionarPosts(Topico post)
        {
            bool retorno = false;
            var strQuery = "";
            strQuery += "INSERT INTO Topico(Titulo,Descricao,Id_Tema,Id_Usuario ,IdTopicoPai,DataCriacao) ";
            strQuery += string.Format("VALUES('{0}','{1}','{2}','{3}','{4}',GETDATE());",
                post.Titulo, post.TopicoFilho.Descricao, post.Tema.Id, post.TopicoFilho.usuario.Id, post.Id);

            using (contexto = new Contexto())
            {
                retorno = contexto.ExecutarInsert(strQuery);

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

        public async Task<Topico> ValTopico(Topico topico)
        {
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                var strQuery = string.Format(" SELECT * FROM Topico WHERE Id ='{0}' AND IdTopicoPai IS NULL", topico.Id);
                reader = await contexto.ExecutaComandoComRetorno(strQuery);
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
            strQuery += string.Format("UPDATE Topico SET DataUpdate = GETDATE() WHERE Id='{0}'", post.Id);
            using (contexto = new Contexto())
            {
                return contexto.ExecutarInsert(strQuery);
            }
        }


        #endregion

        #region Listar Topicos Pai conforme a pesquisa do usuario
        public async Task<List<Topico>> ListarTopicoPesquisa(string pesquisa)
        {
            var topico = new List<Topico>();
            SqlDataReader reader;

            using (contexto = new Contexto())
            {
                var strQuery = String.Format(" SELECT * FROM Topico WHERE IdTopicoPai IS NULL and Titulo Like '%{0}%' ", pesquisa);
                reader = await contexto.ExecutaComandoComRetorno(strQuery);

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
            strQuery += string.Format("UPDATE Topico SET DataUpdate = GETDATE(),Id_Status= 2 WHERE Id='{0}'", topico.Id);
            using (contexto = new Contexto())
            {
                return contexto.ExecutarInsert(strQuery);
            }
        }

        #endregion
    }
}
