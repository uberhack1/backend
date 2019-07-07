using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UberHack.API.Contracts;
using UberHack.API.Entities;
using UberHack.API.Model;

namespace UberHack.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WorkNetController : ControllerBase
    {
        readonly IBaseRepository<Usuario> _usuarioRepository;
        readonly IBaseRepository<Mensagem> _mensagemRepository;
        readonly IBaseRepository<Chat> _chatRepository;
        readonly IBaseRepository<ChatUsuarios> _chatUsuariosRepository;

        public WorkNetController(
            IBaseRepository<Usuario> usuarioRepository,
            IBaseRepository<Mensagem> mensagemRepository,
            IBaseRepository<Chat> chatRepository,
            IBaseRepository<ChatUsuarios> chatUsuariosRepository)
        {
            _usuarioRepository = usuarioRepository;
            _mensagemRepository = mensagemRepository;
            _chatRepository = chatRepository;
            _chatUsuariosRepository = chatUsuariosRepository;
        }

        [HttpPost]
        public void IniciarChatPrivado(int usuarioId, int usuarioConexaoId)
        {
            var chat = _chatRepository.Insert(new Chat() { TipoChat = TipoChat.privado });
            _chatUsuariosRepository.Insert(new ChatUsuarios() { ChatId = chat.Id, UsuarioId = usuarioId });
            _chatUsuariosRepository.Insert(new ChatUsuarios() { ChatId = chat.Id, UsuarioId = usuarioConexaoId });
        }

        [HttpPost]
        public void EnviarMensagem(int usuarioId, int chatId, string conteudo)
        {
            _mensagemRepository.Insert(new Mensagem()
            {
                ChatId = chatId,
                DataHora = DateTime.Now,
                Conteudo = conteudo,
                UsuarioId = usuarioId
            });
        }

        [HttpGet]
        public UsuarioModel ObterUsuario(int codigoUsuario)
        {
            Usuario usuario = _usuarioRepository.GetQueryable()
            .Include(o => o.Empresa)
            .Include(o => o.Faculdade)
            .Include(o => o.BairroCasa)
            .Include(o => o.ChatUsuarios)
            .Where(o => o.Id == codigoUsuario)
            .First();

            var UsuarioModel = new UsuarioModel(usuario);

            var chatsDoUsuario = usuario.ChatUsuarios.Select(c => c.Id);
            UsuarioModel.Chats = _chatRepository.GetQueryable()
                .Where(o => chatsDoUsuario.Contains(o.Id))
                .Include(o => o.Mensagens)
                .OrderByDescending(o => o.Mensagens.OrderByDescending(m => m.DataHora))
                .ToList();

            foreach (var chat in UsuarioModel.Chats)
                chat.Mensagens = chat.Mensagens.OrderBy(o => o.DataHora);

            UsuarioModel.PossiveisConexoes = ObterPossiveisConexoes(usuario);

            return UsuarioModel;
        }

        [HttpGet]
        public Chat ObterChat(int codigoChat)
        {
            var chat = _chatRepository.GetQueryable()
            .Include(o => o.ChatUsuarios)
            //.Include(o => o.ChatUsuarios.Select(u => u.Usuario))
            .Include(o=> o.Mensagens)
            .Where(o => o.Id == codigoChat).First();

            foreach (var chatUsuario in chat.ChatUsuarios)
                chatUsuario.Usuario = _usuarioRepository.GetQueryable().Where(o => o.Id == chatUsuario.UsuarioId).First();

            chat.Mensagens = chat.Mensagens.OrderBy(o => o.DataHora);
            return chat;
        }

        private IEnumerable<PossivelConexaoModel> ObterPossiveisConexoes(Usuario usuario)
        {
            IEnumerable<Usuario> possiveisConexoes = _usuarioRepository.GetAll()
                .Where(o => o.FaculdadeId == usuario.FaculdadeId
                || o.EmpresaId == usuario.EmpresaId
                && o.Id != usuario.Id);

            return possiveisConexoes.Select(o => new PossivelConexaoModel(o));
        }
    }
}
