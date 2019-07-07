using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UberHack.API.Entities;

namespace UberHack.API.Model
{
    public class PossivelConexaoModel
    {
        private Usuario o;

        public PossivelConexaoModel(Usuario usuario)
        {
            this.Nome = usuario.Nome;
            this.Foto = usuario.Foto;
            this.Bio = usuario.Bio;
            this.UsuarioId = usuario.Id;
        }

        public string Nome { get; set; }
        public string Foto { get; set; }
        public string Bio { get; set; }
        public int UsuarioId { get; set; }
    }
}
