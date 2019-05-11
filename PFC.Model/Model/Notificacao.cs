namespace PFC.Model
{
    public class Notificacao
    {
        public int Id { get; set; }
        public Usuario Id_Usu_Sol { get; set; } = new Usuario();
        public Solicitacao Solicitacao { get; set; } = new Solicitacao();
        public Denuncia Denuncia { get; set; } = new Denuncia();

    }
}
