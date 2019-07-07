using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using UberHack.API.Contracts;
using UberHack.API.Entities;

namespace UberHack.API.Hub
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly IBaseRepository<Mensagem> _mensagemRepository;
        private readonly IBaseRepository<Chat> _chatRepository;

        public ChatHub()
        {
        }

        public Task SendMessage(ChatMessage message)
        {
            var data = DateTime.Now;
            return Task.WhenAll(
                Task.Run(() => _mensagemRepository.Insert(new Mensagem()
                {
                    ChatId = message.ChatId,
                    UsuarioId = message.UsuarioId,
                    DataHora = data,
                    Conteudo = message.Content
                })),
                Clients.Group(message.ChatId.ToString())
                    .SendAsync("ReceiveMessage", data, message.UsuarioId, message.Content));
        }

        public Task Join(string groupId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }
    }
}