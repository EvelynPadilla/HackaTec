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

        // Para mensajes de texto (2 argumentos)
        // Un solo método: acepta 3 parámetros (el tercero opcional en el sentido de que puede ser null)
        public async Task SendMessageToRoom(int idSala, string contenido, string? rutaImagen)
        {
            try
            {
                Console.WriteLine($"SendMessageToRoom: idSala={idSala}, contenido={contenido}, rutaImagen={rutaImagen}");

                var userIdClaim = Context.User?.FindFirst(ClaimTypes.NameIdentifier);
                var userTypeClaim = Context.User?.FindFirst(ClaimTypes.Role);

                if (userIdClaim == null || userTypeClaim == null) return;

                int idRemitente = int.Parse(userIdClaim.Value);
                string remitenteTipo = userTypeClaim.Value;

                bool isValid = await _chatService.IsUserInRoomAsync(idSala, idRemitente, remitenteTipo);
                if (!isValid) return;

                var mensaje = await _chatService.SaveMessageAsync(idSala, remitenteTipo, idRemitente, contenido, rutaImagen);

                await Clients.Group($"room_{idSala}").SendAsync("ReceiveMessage", new
                {
                    mensaje.Id,
                    mensaje.RemitenteTipo,
                    mensaje.IdRemitente,
                    mensaje.Contenido,
                    mensaje.RutaImagen,
                    FechaEnvio = mensaje.FechaEnvio,
                    mensaje.EstadoLeido,
                   
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task UserTyping(int idSala, bool isTyping)
        {
            await Clients.OthersInGroup($"room_{idSala}").SendAsync("UserTyping", isTyping);
        }
    }
}
