using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Html;

namespace PFC.Model
{
    public class Topico
    {
        public int Id { get; set; }   
        public Usuario usuario { get; set; } = new Usuario();
        public Tema Tema { get; set; } = new Tema();
        public virtual List<Topico> Resposta { get; set; }
        public Topico TopicoFilho { get; set; }
        public int IdTopicoPai { get; set; }
        public StatusTopico Status { get; set; }   = new StatusTopico();

        [Required]
        public string Titulo { get; set; }
        public DateTime DataCria { get; set; }        
        public string Descricao { get; set; }

        public double pontos { get; set; }
        public double PontosResposta { get; set; }
        public Avaliacao avaliacao { get; set; } = new Avaliacao();


    }
}

