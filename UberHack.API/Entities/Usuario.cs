using System.Collections.Generic;

namespace UberHack.API.Entities
{
    public class Usuario : Entidade
    {
        public IEnumerable<Chat> Chats { get; set; }
        public Empresa Empresa { get; set; }
        public Bairro BairroCasa { get; set; }
        public Faculdade Faculdade { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Cpf { get; set; }
        public int EmpresaId { get; set; }
        public int BairroCasaId { get; set; }
        public int FaculdadeId { get; set; }
        public string Bio { get; set; }
        public string Foto { get; set; }
    }
}
