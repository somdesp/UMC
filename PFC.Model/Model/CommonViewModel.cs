using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFC.Model
{
    public class CommonViewModel
    {
        public Usuario usuario = new Usuario();
        public IEnumerable<Topico> topico;
    }
}
