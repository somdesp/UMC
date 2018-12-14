using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PFC.DAO;
using PFC.Model;

namespace PFC.Business
{
    public class AutorizaçoesBLL
    {
        AutorizaçoesDAO permisaoDao = new AutorizaçoesDAO();

        public Autorizaçoes ReturnAutPorID(Autorizaçoes auth)
        {
            return permisaoDao.ReturnAutPorID(auth);
        }

    }
}
