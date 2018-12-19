using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PFC.Model
{
    public class Anexos
    {
        public int Id { get; set; }
        public string Caminho { get; set; }
        public HttpPostedFileBase ArquivoBase { get; set; }
    }
}
