using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PFC.Model
{
    public class Tema
    {
        public int Id { get; set; }
        public Usuario usuario { get; set; } = new Usuario();
        public string Nome { get; set; }

    }
}
