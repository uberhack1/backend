using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UberHack.API.Entities;

namespace UberHack.API.Model
{
    public class UsuarioModel
    {
        public Usuario Usuario { get; set; }
        public IEnumerable<Chat> Chats { get; set; }

        public IEnumerable<PossivelConexaoModel> PossiveisConexoes { get; set; }

        public UsuarioModel(IEnumerable<PossivelConexaoModel> possiveisConexoes)
        {
            PossiveisConexoes = possiveisConexoes;
        }

        public UsuarioModel(Usuario usuario)
        {
            this.Usuario = usuario;
        }
    }
}
