using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFC.DAO
{
    public class ChatDAO
    {
        private Contexto contexto;

        public void addMessagem(string message, int Id_Usuario_Post, int Id_Usuario_Put)
        {
            try
            {
                var strQuery = "";
                strQuery += "INSERT INTO Chat (Mensagem,Id_Usuario_Post,Id_Usuario_Put) ";
                strQuery += string.Format("VALUES('{0}','{1}','{2}')",
                    message, Id_Usuario_Post, Id_Usuario_Put);

                using (contexto = new Contexto())
                {

                    contexto.ExecutarInsert(strQuery);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
