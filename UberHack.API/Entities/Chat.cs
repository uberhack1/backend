using System.Collections.Generic;

namespace UberHack.API.Entities
{
    public class Chat : Entidade
    {
        public TipoChat TipoChat { get; set; }
        public IEnumerable<Mensagem> Mensagens { get; set; }
        public IEnumerable<ChatUsuarios> ChatUsuarios { get; set; }
        public string Nome { get; set; }
    }
}