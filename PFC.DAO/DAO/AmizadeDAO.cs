using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PFC.Model;

namespace PFC.DAO
{
    public class AmizadeDAO
    {
        private Contexto contexto;


        public bool solicitacaoAmizade(Usuario usuario, Usuario usuarioSolicitado)
        {
            var strQuery = "";
            strQuery += "INSERT INTO Amizade (Id_Usu_Sol,Id_Usu_Pen,Amizade_pend) ";
            strQuery += string.Format("VALUES('{0}','{1}',0)",
                usuario.Id, usuarioSolicitado.Id);

            using (contexto = new Contexto())
            {
                return contexto.ExecutarInsert(strQuery);
            }

        }

        public bool ValidaAmizade(Usuario usuario, Usuario usuarioSolicitado)
        {
            SqlDataReader reader;

            using (contexto = new Contexto())
            {

                string strQuery = string.Format("select * from Amizade WHERE Id_Usu_Sol = '{0}' AND Id_Usu_Pen='{1}' ",
                    usuario.Id, usuarioSolicitado.Id);
                reader = contexto.ExecutaComandoComRetorno(strQuery);

                if (reader.HasRows.Equals(false))
                {
                    return false;
                }


            }
            reader.Close();
            return true;

        }


        
    }
}
