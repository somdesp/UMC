using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFC.Model
{
    public class Curso
    {
        public int Id { get; set; }
        public string curso { get; set; }

        public ICollection<Semestre> Turma { get; set; }

    }
}
