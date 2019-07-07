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
            .First(o => o.Id == codigoUsuario);

            var usuarioModel = new UsuarioModel(usuario);

            var chatsDoUsuario = usuario.ChatUsuarios.Select(c => c.Id);
            usuarioModel.Chats = _chatRepository.GetQueryable()
                .Where(o => chatsDoUsuario.Contains(o.Id))
                .Include(o => o.Mensagens)
                .ToList();

            foreach (var chat in usuarioModel.Chats)
                chat.Mensagens = chat.Mensagens.OrderBy(o => o.DataHora);

            usuarioModel.PossiveisConexoes = ObterPossiveisConexoes(usuario);

            return usuarioModel;
        }

        [HttpGet]
        public Chat ObterChat(int codigoChat)
        {
            var chat = _chatRepository.GetQueryable()
            .Include(o => o.ChatUsuarios)
            .Include(o => o.Mensagens)
            .First(o => o.Id == codigoChat);

            var usuarios = _usuarioRepository.GetQueryable().Where(u => chat.ChatUsuarios.Any(c => c.UsuarioId == u.Id)).ToList();
            
            chat.Mensagens = chat.Mensagens.OrderBy(o => o.DataHora);

            foreach (var item in chat.Mensagens)
            {
                item.Usuario = usuarios.FirstOrDefault(u => u.Id == item.UsuarioId);
            }

            return chat;
        }

        private IEnumerable<PossivelConexaoModel> ObterPossiveisConexoes(Usuario usuario)
        {
            IEnumerable<Usuario> possiveisConexoes = _usuarioRepository.GetQueryable()
                .Where(o => o.FaculdadeId == usuario.FaculdadeId
                || o.EmpresaId == usuario.EmpresaId
                && o.Id != usuario.Id).Include(u => u.Empresa).ToList();

            return possiveisConexoes.Select(o => new PossivelConexaoModel(o));
        }
    }
}
