
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PFC.Model
{
    public class Avaliacao
    {
        public int idAvaliacao { get; set; }
        public int  idUsuario { get; set; }
        public int idTopico { get; set; }
        public float pontos { get; set; }
        public float mediaPontos { get; set; }
        public int contarLike { get; set; }
        public int contarDeslike { get; set; }
    }
}