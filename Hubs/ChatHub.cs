using HackaTec.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace HackaTec.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;

        public ChatHub(ChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task JoinRoom(int idSala)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"room_{idSala}");
        }

        public async Task LeaveRoom(int idSala)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"room_{idSala}");
        }

        public async Task SendMessageToRoom(int idSala, string contenido, string? rutaImagen = null)
        {
            var userIdClaim = Context.User?.FindFirst(ClaimTypes.NameIdentifier);
            var userTypeClaim = Context.User?.FindFirst("UserType");

            if (userIdClaim == null || userTypeClaim == null) return;

            int idRemitente = int.Parse(userIdClaim.Value);
            string remitenteTipo = userTypeClaim.Value;

            // Validar que el usuario pertenece a la sala
            bool isValid = await _chatService.IsUserInRoomAsync(idSala, idRemitente, remitenteTipo);
            if (!isValid) return;

            // Guardar mensaje en BD
            var mensaje = await _chatService.SaveMessageAsync(idSala, remitenteTipo, idRemitente, contenido, rutaImagen);

            // Enviar a todos los miembros de la sala
            await Clients.Group($"room_{idSala}").SendAsync("ReceiveMessage", new
            {
                mensaje.Id,
                mensaje.RemitenteTipo,
                mensaje.IdRemitente,
                mensaje.Contenido,
                mensaje.RutaImagen,
                FechaEnvio = mensaje.FechaEnvio,
                mensaje.EstadoLeido
            });
        }

        public async Task UserTyping(int idSala, bool isTyping)
        {
            await Clients.OthersInGroup($"room_{idSala}").SendAsync("UserTyping", isTyping);
        }
    }
}
