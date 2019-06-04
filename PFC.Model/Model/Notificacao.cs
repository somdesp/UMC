namespace PFC.Model
{
    public class Notificacao
    {
        public int Id { get; set; }
        public Usuario Id_Usu_Sol { get; set; } = new Usuario();
        public Amizade Solicitacao { get; set; } = new Amizade();
        public Denuncia Denuncia { get; set; } = new Denuncia();

    }
}
