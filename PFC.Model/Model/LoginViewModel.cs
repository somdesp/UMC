using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFC.Model
{
    public class LoginViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public string Nome { get; set; }

        public string ReturnUrl { get; set; }

        public bool success { get; set; }

        public Autorizacoes Auth { get; set; } = new Autorizacoes();
        public Anexos UploadArquivo { get; set; } = new Anexos();
    }
}
