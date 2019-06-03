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
                topico.Titulo, topico.Tema.Id, topico.usuario.Id, topico.Descricao, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), topico.Status.Id);

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
                    + "INNER JOIN Usuario usu ON usu.Id = toc.Id_Usuario WHERE toc.Id_Status <> 4 AND IdTopicoPai IS NULL ORDER BY DataUpdate DESC";
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
            try
            {
                using (contexto = new Contexto())
                {
                    var strQuery = string.Format(" SELECT toc.Id  As IdTopico,toc.Titulo,toc.Id_Tema,toc.Id_Usuario As IdUsuario,toc.IdTopicoPai," +
                        "toc.Id_Status,toc.Id_Status, toc.Descricao, toc.DataCriacao, usu.Id AS IdUsuario, usu.Nome, usu.Login, usu.Email, usu.Senha, usu.RGM," +
                        "usu.DataNasci, usu.DataCad, usu.Id_Curso, usu.Id_Semestre, usu.Id_Genero, usu.Id_Arquivo, usu.Id_Permissoes AS IdTopico ,arq.Arquivo FROM Topico toc" +
                       " INNER JOIN Usuario usu ON usu.Id = toc.Id_Usuario INNER JOIN Arquivos arq ON arq.Id = usu.Id_Arquivo WHERE toc.IdTopicoPai = {0} ", idTopico);
                    reader = await contexto.ExecutaComandoComRetorno(strQuery);

                    while (reader.Read())
                    {
                        Topico readerTopico = new Topico();
                        readerTopico.IdTopicoPai = Convert.ToInt32(reader["IdTopicoPai"].ToString());
                        readerTopico.Id = Convert.ToInt32(reader["IdTopico"].ToString());
                        readerTopico.Titulo = reader["Titulo"].ToString();
                        readerTopico.DataCria = Convert.ToDateTime(reader["DataCriacao"].ToString());
                        readerTopico.Descricao = reader["Descricao"].ToString();
                        readerTopico.usuario.Id = Convert.ToInt32(reader["IdUsuario"].ToString());
                        readerTopico.usuario.Nome = reader["Nome"].ToString();
                        readerTopico.usuario.Login = reader["Login"].ToString();
                        readerTopico.usuario.Email = reader["Email"].ToString();
                        readerTopico.usuario.RGM = reader["RGM"].ToString();
                        readerTopico.usuario.UploadArquivo.Caminho = (reader["Arquivo"].ToString());
                        readerTopico.usuario.DataNasci = Convert.ToDateTime(reader["DataNasci"].ToString());
                        readerTopico.usuario.Curso.Id = Convert.ToInt32(reader["Id_Curso"].ToString());
                        readerTopico.usuario.Semestre.Id = Convert.ToInt32(reader["Id_Semestre"].ToString());
                        readerTopico.usuario.Sexo.Id = Convert.ToInt32(reader["Id_Genero"].ToString());
                        readerTopico.Tema.Id = Convert.ToInt32(reader["Id_Tema"].ToString());
                        readerTopico.Status.Id = Convert.ToInt32(reader["Id_Status"].ToString());

                        topico.Add(readerTopico);
                    }
                }
                reader.Close();
                return topico;
            }
            catch (Exception ex)
            {

                throw;
            }

           
        }
        #endregion

        #region Detalhe Topico
        public async Task<Topico> DetalheTopico(Topico topico)
        {
            SqlDataReader reader;
            try
            {
                using (contexto = new Contexto())
                {
                    var strQuery = string.Format(" SELECT toc.Id  As IdTopico,toc.Titulo,toc.Id_Tema,toc.Id_Usuario As IdUsuario,toc.IdTopicoPai," +
                        "toc.Id_Status, toc.Descricao, toc.DataCriacao, usu.Id AS IdUsuario, usu.Nome, usu.Login, usu.Email, usu.Senha, usu.RGM," +
                        "usu.DataNasci, usu.DataCad, usu.Id_Curso, usu.Id_Semestre, usu.Id_Genero, usu.Id_Arquivo, usu.Id_Permissoes AS IdTopico ,arq.Arquivo FROM Topico toc" +
                       " INNER JOIN Usuario usu ON usu.Id = toc.Id_Usuario INNER JOIN Arquivos arq ON arq.Id = usu.Id_Arquivo WHERE toc.Id = {0} ", topico.Id);
                    reader = await contexto.ExecutaComandoComRetorno(strQuery);

                    while (reader.Read())
                    {

                        Topico readerTopico = new Topico();
                        readerTopico.Id = Convert.ToInt32(reader["IdTopico"].ToString());
                        readerTopico.Titulo = reader["Titulo"].ToString();
                        readerTopico.DataCria = Convert.ToDateTime(reader["DataCriacao"].ToString());
                        readerTopico.Descricao = reader["Descricao"].ToString();
                        readerTopico.usuario.Id = Convert.ToInt32(reader["IdUsuario"].ToString());
                        readerTopico.usuario.Nome =(reader["Nome"].ToString());
                        readerTopico.usuario.Login = (reader["Login"].ToString());
                        readerTopico.usuario.Email = (reader["Email"].ToString());
                        readerTopico.usuario.UploadArquivo.Caminho = (reader["Arquivo"].ToString());
                        readerTopico.Tema.Id = Convert.ToInt32(reader["Id_Tema"].ToString());
                        readerTopico.Status.Id = Convert.ToInt32(reader["Id_Status"].ToString());
                        topico = (readerTopico);

                    }
                }
                reader.Close();
                return topico;
            }
            catch (Exception ex)
            {

                throw;
            }


        }
        #endregion

        #region Adicionar Posts (Respostas)
        public bool AdicionarPosts(Topico post)
        {
            bool retorno = false;
            var strQuery = "";
            strQuery += "INSERT INTO Topico(Titulo,Descricao,Id_Tema,Id_Usuario ,IdTopicoPai,DataCriacao,Id_Status) ";
            strQuery += string.Format("VALUES('{0}','{1}','{2}','{3}','{4}','{5}',{6});",
                post.Titulo, post.TopicoFilho.Descricao, post.Tema.Id, post.TopicoFilho.usuario.Id,
                post.Id, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), post.TopicoFilho.Status.Id);

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
            strQuery += string.Format("UPDATE Topico SET DataUpdate = '{1}' WHERE Id='{0}'", post.Id,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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
            strQuery += string.Format("UPDATE Topico SET DataUpdate = GETDATE(),Id_Status= 2 ,Titulo='{1}' WHERE Id='{0}'", topico.Id,topico.Titulo);
            using (contexto = new Contexto())
            {
                return contexto.ExecutarInsert(strQuery);
            }
        }

        #endregion


        #region RemoverResposta
        public bool RemoverResposta(Denuncia denuncia)
        {
            var strQuery = "";
            strQuery += string.Format("UPDATE Topico SET DataUpdate = GETDATE(),Id_Status= 4 WHERE Id={0}", denuncia.Topico.Id);
            using (contexto = new Contexto())
            {
                return contexto.ExecutarInsert(strQuery);
            }
        }

        #endregion

        #region GetTopicosPesquisa
        public async Task<List<Topico>> GetTopicosPesquisa(dynamic topicoPesquisa)
        {
            var topico = new List<Topico>();
            SqlDataReader reader;
            try
            {
                using (contexto = new Contexto())
                {
                    var strQuery = string.Format(" SELECT toc.Id ,toc.Titulo,toc.Id_Tema,toc.Id_Usuario,toc.IdTopicoPai,"
                        + "toc.Id_Status,toc.Descricao,toc.DataCriacao,usu.Id AS IdUsuario,usu.Nome,usu.Login,usu.Email, usu.Senha, usu.RGM, "
                        + "usu.DataNasci, usu.DataCad, usu.Id_Curso ,usu.Id_Semestre,usu.Id_Genero,  usu.Id_Arquivo,usu.Id_Permissoes AS IdTopico FROM Topico toc "
                        + "INNER JOIN Usuario usu ON usu.Id = toc.Id_Usuario  WHERE toc.DataCriacao >='{0}' AND toc.DataCriacao <='{1}' AND  toc.Id_Status = {2} AND toc.IdTopicoPai IS NULL ORDER BY DataUpdate DESC", topicoPesquisa.datAbertIni.ToString("yyyy-MM-dd"), topicoPesquisa.datAbertFim.ToString("yyyy-MM-dd"), topicoPesquisa.status);

                    reader = await contexto.ExecutaComandoComRetorno(strQuery);

                    while (reader.Read())
                    {
                        Topico readerTopico = new Topico();
                        readerTopico.Id = Convert.ToInt32(reader["Id"].ToString());
                        readerTopico.Titulo = reader["Titulo"].ToString();
                        readerTopico.DataCria = Convert.ToDateTime(reader["DataCriacao"].ToString());
                        readerTopico.Descricao = reader["Descricao"].ToString();
                        readerTopico.Tema.Id = Convert.ToInt32(reader["Id_Tema"].ToString());
                        readerTopico.Status.Id = Convert.ToInt32(reader["Id_Status"].ToString());
                        
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
            catch (Exception ex)
            {

                throw;
            }

        }
        #endregion

        #region GetStatusTopico
        public async Task<List<StatusTopico>> GetStatusTopico()
        {
            var statustopico = new List<StatusTopico>();
            SqlDataReader reader;
            try
            {
                using (contexto = new Contexto())
                {
                    var strQuery = string.Format(" SELECT * FROM StatusTopicos");

                    reader = await contexto.ExecutaComandoComRetorno(strQuery);

                    while (reader.Read())
                    {
                        StatusTopico readerTopico = new StatusTopico();
                        readerTopico.Id = Convert.ToInt32(reader["Id"].ToString());
                        readerTopico.Status = reader["Status"].ToString();

                        statustopico.Add(readerTopico);
                    }
                }
                reader.Close();
                return statustopico;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        #endregion

        #region AlterarTopico
        public async Task<bool> AlterarTopico(Topico topico)
        {

            try
            {
                var strQuery = "";
                strQuery = string.Format(" UPDATE Topico SET Id_Status ={0},DataUpdate = '{1}' WHERE ID = {2} ",
                    topico.Status.Id, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), topico.Id);

                using (contexto = new Contexto())
                {
                    return contexto.ExecutarInsert(strQuery);
                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }
        #endregion
    }
}
