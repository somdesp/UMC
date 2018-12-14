using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PFC.Model
{
    public class Semestre
    {
        public int Id { get; set; }
        public string semestre { get; set; }
        public Curso Curso { get; set; } = new Curso();

    }
}
