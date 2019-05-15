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

        public string ConnectionId { get; set; }
        public List<LoginViewModel> friendsList { get; set; }
        public string fontColor { get; set; }
        public string sex { get; set; }
        public int age { get; set; }
        public string status { get; set; }
        public string memberType { get; set; }
        public string avator { get; set; }

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

    public enum Status
    {
        Online,
        Away,
        Busy,
        Offline
    }
}
