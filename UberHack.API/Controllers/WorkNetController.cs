﻿using Microsoft.AspNetCore.Mvc;
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
            Usuario usuario = _usuarioRepository.Get(codigoUsuario);

            var UsuarioModel = new UsuarioModel(usuario);

            UsuarioModel.Chats = usuario.ChatUsuarios
                .Select(o => o.Chat)
                .OrderByDescending(o => o.Mensagens.OrderByDescending(m => m.DataHora));

            foreach (var chat in UsuarioModel.Chats)
                chat.Mensagens = chat.Mensagens.OrderBy(o => o.DataHora);

            UsuarioModel.PossiveisConexoes = ObterPossiveisConexoes(codigoUsuario);

            return UsuarioModel;
        }

        private IEnumerable<PossivelConexaoModel> ObterPossiveisConexoes(int usuarioId)
        {
            Usuario usuarioLogado = _usuarioRepository.Get(usuarioId);

            IEnumerable<Usuario> possiveisConexoes = _usuarioRepository.GetAll()
                .Where(o => o.FaculdadeId == usuarioLogado.FaculdadeId
                || o.EmpresaId == usuarioLogado.EmpresaId
                && o.Id != usuarioId);

            return possiveisConexoes.Select(o => new PossivelConexaoModel(o));
        }
    }
}
