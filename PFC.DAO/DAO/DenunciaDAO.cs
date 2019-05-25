﻿using PFC.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PFC.DAO
{
    public class DenunciaDAO
    {
        private Contexto contexto;

        #region DenunciaUsuario
        public async Task<bool> DenunciaUsuarioAsync(Denuncia denuncia)
        {
            SqlDataReader reader;

            var strQuery = "";
            strQuery += "INSERT INTO Denuncia (Id_Usu_Sol,Id_Usu_Pen,Descricao,Resposta,Status,Id_Topico,DataCria,Id_topicoFilho) ";
            strQuery += string.Format("VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}');SELECT SCOPE_IDENTITY() AS Id_Denuncia",
                denuncia.Id_Usu_Sol.Id, denuncia.Id_Usu_Pen.Id, denuncia.Descricao, denuncia.Resposta, 1, denuncia.Topico.IdTopicoPai, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), denuncia.Topico.Id);


            try
            {
                int IdDenuncia = 0;
                using (contexto = new Contexto())
                {
                    reader = await contexto.ExecutaComandoComRetorno(strQuery);
                    strQuery = "";
                    while (reader.Read())
                    {
                        IdDenuncia = Convert.ToInt32(reader["Id_Denuncia"].ToString());
                        strQuery += string.Format("INSERT INTO Notificacao (Id_Denuncia,Status)VALUES({0},1)", IdDenuncia);

                    }
                    reader.Close();


                    contexto.ExecutarInsert(strQuery);

                }
                return true;
            }
            catch (Exception EX)
            {

                throw;
            }



        }

        #endregion

        #region RemoverRespostaAsync
        public async Task<bool> RemoverRespostaAsync(Denuncia denuncia)
        {
            SqlDataReader reader;

            var strQuery = "";

            strQuery = string.Format("SELECT * FROM Denuncia WHERE Id_topico ={0}", denuncia.Topico.Id);

            try
            {
                int IdDenuncia = 0;
                using (contexto = new Contexto())
                {
                    reader = await contexto.ExecutaComandoComRetorno(strQuery);
                    strQuery = "";

                    if (reader.HasRows.Equals(false))
                    {
                        reader.Close();

                        strQuery = "INSERT INTO Denuncia (Id_Usu_Sol,Id_Usu_Pen,Descricao,Resposta,Status,Id_Topico,DataCria) ";
                        strQuery += string.Format("VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                            denuncia.Id_Usu_Sol.Id, denuncia.Id_Usu_Pen.Id, denuncia.Descricao, denuncia.Resposta, 0, denuncia.Topico.Id, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        contexto.ExecutarInsert(strQuery);

                    }
                    else
                    {
                        while (reader.Read())
                        {
                            IdDenuncia = Convert.ToInt32(reader["Id_Denuncia"].ToString());
                        }
                        reader.Close();

                        strQuery = string.Format("UPDATE Denuncia SET Resposta={0},Status=0 WHERE Id = {1} ; UPDATE Notificacao SET Status =0 WHERE Id_Denuncia = {1}", denuncia.Resposta, IdDenuncia);
                        contexto.ExecutarInsert(strQuery);
                    }

                }
                return true;
            }
            catch (Exception EX)
            {

                throw;
            }



        }

        #endregion

        #region RemoverRespostaAsync
        public async Task<bool> ValidaDenunciaAs(Denuncia denuncia)
        {
            SqlDataReader reader;

            var strQuery = "";
            strQuery += "INSERT INTO Denuncia (Id_Usu_Sol,Id_Usu_Pen,Descricao,Resposta,Status,Id_Topico,DataCria) ";
            strQuery += string.Format("VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}');SELECT SCOPE_IDENTITY() AS Id_Denuncia",
                denuncia.Id_Usu_Sol.Id, denuncia.Id_Usu_Pen.Id, denuncia.Descricao, denuncia.Resposta, 1, denuncia.Topico.Id, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            try
            {
                int IdDenuncia = 0;
                using (contexto = new Contexto())
                {
                    reader = await contexto.ExecutaComandoComRetorno(strQuery);
                    strQuery = "";
                    while (reader.Read())
                    {
                        IdDenuncia = Convert.ToInt32(reader["Id_Denuncia"].ToString());
                        strQuery += string.Format("INSERT INTO Notificacao (Id_Denuncia,Status)VALUES({0},1)", IdDenuncia);

                    }
                    reader.Close();


                    contexto.ExecutarInsert(strQuery);

                }
                return true;
            }
            catch (Exception EX)
            {

                throw;
            }



        }

        #endregion

        #region ListaDenuncia
        public async Task<List<Denuncia>> ListaDenuncia()
        {
            SqlDataReader reader;

            List<Denuncia> denuncaArray = new List<Denuncia>();

            var strQuery = "";
            strQuery = string.Format("SELECT usu.Nome,usu.Id AS IdUsuario,de.Id_topicoFilho,de.Id AS IdDenuncia, de.Descricao," +
                "de.Id_topico,de.DataCria,de.Resposta,de.Status FROM Denuncia de " +
                    "INNER JOIN Usuario usu ON usu.Id = de.Id_Usu_Sol");

            try
            {

                using (contexto = new Contexto())
                {
                    reader = await contexto.ExecutaComandoComRetorno(strQuery);
                    strQuery = "";
                    while (reader.Read())
                    {
                        var temObjeto = new Denuncia();
                        temObjeto.Id_Usu_Sol.Nome = reader["Nome"].ToString();
                        temObjeto.Id_Usu_Sol.Id = Convert.ToInt16(reader["IdUsuario"].ToString());
                        temObjeto.Id = Convert.ToInt16(reader["IdDenuncia"].ToString());
                        temObjeto.Descricao = reader["Descricao"].ToString();
                        temObjeto.Resposta = reader["Resposta"].ToString();
                        temObjeto.Topico.IdTopicoPai = Convert.ToInt16(reader["Id_Topico"].ToString());
                        temObjeto.Topico.Id = (reader["Id_topicoFilho"].ToString() =="" ) ? temObjeto.Topico.IdTopicoPai : Convert.ToInt16(reader["Id_topicoFilho"].ToString());


                        temObjeto.Status = Convert.ToBoolean(reader["Status"].ToString());
                        temObjeto.DataCria = Convert.ToDateTime(reader["DataCria"].ToString()).Date;

                        denuncaArray.Add(temObjeto);
                    }
                    reader.Close();



                }
                return denuncaArray;
            }
            catch (Exception EX)
            {

                throw;
            }



        }

        #endregion




    }
}
