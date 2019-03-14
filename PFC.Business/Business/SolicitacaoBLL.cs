﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PFC.DAO;
using PFC.Model;

namespace PFC.Business
{
    public class SolicitacaoBLL
    {
        SolicitacaoDAO amizadeDao = new SolicitacaoDAO();
        public bool solicitacaoAmizade(Usuario usuario, Usuario usuarioSolicitado)
        {

            return amizadeDao.solicitacaoAmizade(usuario, usuarioSolicitado);
        }

        public bool ValidaAmizade(Usuario usuario, Usuario usuarioSolicitado)
        {
            return amizadeDao.ValidaAmizade(usuario, usuarioSolicitado);
        }

        
    }
}