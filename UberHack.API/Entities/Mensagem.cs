using System;

namespace UberHack.API.Entities
{
    public class Mensagem : Entidade
    {
        public Usuario Usuario { get; set; }
        public Chat Chat { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataHora { get; set; }
        public int UsuarioId { get; set; }
        public int ChatId { get; set; }
    }
}