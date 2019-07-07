using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UberHack.API.Contracts;
using UberHack.API.Entities;

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
        public ActionResult<IEnumerable<Usuario>> ObterPossiveisConexoes()
        {
            Usuario usuarioLogado = ObterUsuarioLogado();

            IEnumerable<Usuario> possiveisConexoes = _usuarioRepository.GetAll()
                .Where(o => o.FaculdadeId == usuarioLogado.FaculdadeId || o.EmpresaId == usuarioLogado.EmpresaId);

            return base.Ok(possiveisConexoes);

        }

        [HttpGet]
        public Usuario ObterUsuario()
        {
            Usuario usuario = _usuarioRepository.Get(_codigoUsuarioLogado);

            usuario.Chats = usuario.Chats.OrderByDescending(o => o.Mensagens.OrderByDescending(m => m.DataHora));
            foreach (var chat in usuario.Chats)
                chat.Mensagens = chat.Mensagens.OrderBy(o => o.DataHora);

            return usuario;
        }

        private Usuario ObterUsuarioLogado()
        {
            return _usuarioRepository.Get(_codigoUsuarioLogado);
        }
    }
}
