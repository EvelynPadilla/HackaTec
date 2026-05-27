using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace HackaTec.Hubs
{
    [Authorize] // 🔐 Protege el Hub, solo usuarios autenticados pueden conectar
    public class ChatHub : Hub
    {
        // Diccionario en memoria para almacenar la relación UserId -> ConnectionId(s)
        // NOTA: Esto no escala en web farms. Para producción, usa una caché distribuida (Redis) o una DB.
        private static readonly ConcurrentDictionary<string, List<string>> _userConnections = new();

        // Este método se llama automáticamente cuando un cliente se conecta
        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                // Añade la nueva connectionId a la lista del usuario
                _userConnections.AddOrUpdate(userId,
                    new List<string> { Context.ConnectionId },
                    (key, existingList) => { existingList.Add(Context.ConnectionId); return existingList; });
            }
            await base.OnConnectedAsync();
        }

        // Se llama cuando un cliente se desconecta
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null && _userConnections.TryGetValue(userId, out var connections))
            {
                connections.Remove(Context.ConnectionId);
                if (connections.Count == 0)
                    _userConnections.TryRemove(userId, out _);
            }
            await base.OnDisconnectedAsync(exception);
        }

        // 📨 Método para enviar un mensaje a un usuario específico
        public async Task SendPrivateMessage(string receiverId, string message)
        {
            var senderId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(senderId)) return;

            // Busca todas las conexiones activas del destinatario
            if (_userConnections.TryGetValue(receiverId, out var receiverConnections))
            {
                // Envía el mensaje a cada una de las conexiones del usuario destinatario
                await Clients.Clients(receiverConnections).SendAsync("ReceivePrivateMessage", senderId, message);
            }

            // (Opcional) También podrías confirmar al remitente que el mensaje fue enviado
            // await Clients.Caller.SendAsync("MessageSentConfirmation", receiverId, message);
        }
    }
}
