using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UberHack.API.Contracts;
using UberHack.API.Entities;
using UberHack.API.Model;

namespace UberHack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkNetController : ControllerBase
    {
        // Mock persona Bianca
        private readonly int _codigoUsuarioLogado = 1;

        IBaseRepository<Usuario> _usuarioRepository;
        readonly IBaseRepository<Chat> _chatRepository;

        public WorkNetController(
            IBaseRepository<Usuario> usuarioRepository,
            IBaseRepository<Chat> chatRepository)
        {
            _usuarioRepository = usuarioRepository;
            _chatRepository = chatRepository;
        }

        [HttpGet]
        public UsuarioModel ObterUsuario(int codigoUsuario)
        {
            Usuario usuario = _usuarioRepository.Get(codigoUsuario);

            var UsuarioModel = new UsuarioModel(usuario);

            usuario.Chats = usuario.Chats.OrderByDescending(o => o.Mensagens.OrderByDescending(m => m.DataHora));
            foreach (var chat in usuario.Chats)
                chat.Mensagens = chat.Mensagens.OrderBy(o => o.DataHora);

            UsuarioModel.PossiveisConexoes = ObterPossiveisConexoes(codigoUsuario);

            return UsuarioModel;
        }

        private IEnumerable<PossivelConexaoModel> ObterPossiveisConexoes(int usuarioId)
        {
            Usuario usuarioLogado = ObterUsuarioLogado();

            IEnumerable<Usuario> possiveisConexoes = _usuarioRepository.GetAll()
                .Where(o => o.FaculdadeId == usuarioLogado.FaculdadeId 
                || o.EmpresaId == usuarioLogado.EmpresaId
                && o.Id != usuarioId);

            return possiveisConexoes.Select(o => new PossivelConexaoModel(o));
        }

        private Usuario ObterUsuarioLogado()
        {
            return _usuarioRepository.Get(_codigoUsuarioLogado);
        }
    }
}
