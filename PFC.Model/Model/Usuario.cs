using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PFC.Model
{
    public class Usuario
    {
        /////////////////////////// ATRIBUTOS USUARIOS COM VALIDAÇOES DATAANNOTATIONS*************************
        ////////////////////////////////Formulario de Cadastro*************************
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [Remote("ConsultaUnico", "Usuario", ErrorMessage = "Esse Login já existe no sistema")]
        public string Login { get; set; }

        [Required]
        [Remote("ConsultaUnico", "Usuario", ErrorMessage = "Esse e-mail já existe no sistema")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Favor informe um e-mail válido")]
        public string Email { get; set; }

        [Required]
        public string RGM { get; set; }

        [Required]
        [DataType(DataType.Date)]

        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Remote("ConsultaData", "Usuario", ErrorMessage = "Data Invalida sua idade precisa ser maior que 16")]
        public DateTime DataNasci { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 10 caracteres.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public DateTime DataCad { get; set; }

        /////////////////////////////////Fim Forumalario de cadastro****************************



        /////////////////////////////////Relaçoes com Outras Classes******************************

        [Required]
        public Genero Sexo { get; set; } = new Genero();
        public Curso Curso { get; set; } = new Curso();
        public Semestre Semestre { get; set; } = new Semestre();
        public Autorizaçoes Auth { get; set; } = new Autorizaçoes();
        public Anexos UploadArquivo { get; set; } = new Anexos();
        public Avaliacao avaliacao { get; set; } = new Avaliacao();


        /////////////////////Fim Relaçoes com Outras Classes************************************

    }
}
