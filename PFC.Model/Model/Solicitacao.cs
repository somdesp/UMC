using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFC.Model
{
   public  class Solicitacao
    {
        public string ConnectionId { get; set; }
        public Usuario usuario { get; set; } = new Usuario();
        public Usuario usuarioSolicitado { get; set; } = new Usuario();

    }
}
