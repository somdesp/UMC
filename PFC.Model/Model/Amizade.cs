namespace PFC.Model
{
    public  class Amizade
    {
        public string ConnectionId { get; set; }
        public Usuario usuario { get; set; } = new Usuario();
        public Usuario usuarioSolicitado { get; set; } = new Usuario();

    }
}
