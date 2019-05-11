namespace PFC.Model
{
    public class Denuncia
    {
        public int Id { get; set; }
        public Usuario Id_Usu_Sol { get; set; } = new Usuario();
        public Usuario Id_Usu_Pen { get; set; } = new Usuario();
        public string Descricao { get; set; }
        public string Resposta { get; set; }
        public bool Status { get; set; }
        public Topico Topico { get; set; } = new Topico();

    }
}
