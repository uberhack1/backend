using System.Collections.Generic;

namespace UberHack.API.Entities
{
    public class ChatUsuarios : Entidade
    {
        public int ChatId { get; set; }
        public int UsuarioId { get; set; }
        public Chat Chat { get; set; }
        public Usuario Usuario { get; set; }
    }
}