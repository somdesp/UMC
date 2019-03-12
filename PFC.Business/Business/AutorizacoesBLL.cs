using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PFC.DAO;
using PFC.Model;

namespace PFC.Business
{
    public class AutorizacoesBLL
    {
        AutorizacoesDAO permisaoDao = new AutorizacoesDAO();

        public Autorizacoes ReturnAutPorID(Autorizacoes auth)
        {
            return permisaoDao.ReturnAutPorID(auth);
        }

    }
}
