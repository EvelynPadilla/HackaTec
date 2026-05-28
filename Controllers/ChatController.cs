using HackaTec.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HackaTec.Controllers
{

    public class ChatController : Controller
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<IActionResult> List()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userTypeClaim = User.FindFirstValue(ClaimTypes.Role); // ← cambiado

            if (userIdClaim == null || userTypeClaim == null) return Challenge();

            int userId = int.Parse(userIdClaim);
            string userType = userTypeClaim; // "Donante" o "Institucion"

            var salas = await _chatService.GetUserRoomsAsync(userId, userType);
            var viewModel = new List<ChatListViewModel>();

            foreach (var sala in salas)
            {
                int otroId = userType == "Donante" ? sala.IdInstitucion : sala.IdUsuarioDonante;
                string otroTipo = userType == "Donante" ? "Institucion" : "Donante";
                string otroNombre = await _chatService.GetParticipantNameAsync(otroId, otroTipo);
                var ultimoMensaje = sala.Mensajes.OrderByDescending(m => m.FechaEnvio).FirstOrDefault();

                viewModel.Add(new ChatListViewModel
                {
                    IdSala = sala.Id,
                    OtroParticipanteNombre = otroNombre,
                    UltimoMensaje = ultimoMensaje?.Contenido,
                    FechaUltimoMensaje = ultimoMensaje?.FechaEnvio,
                    IdPostNecesidad = sala.IdPostNecesidad
                });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Index(int idSala)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userTypeClaim = User.FindFirstValue(ClaimTypes.Role);

            if (userIdClaim == null || userTypeClaim == null) return Challenge();

            int userId = int.Parse(userIdClaim);
            string userType = userTypeClaim;

            bool isMember = await _chatService.IsUserInRoomAsync(idSala, userId, userType);
            if (!isMember) return Forbid();

            var historial = await _chatService.GetChatHistoryAsync(idSala);
            await _chatService.MarkMessagesAsReadAsync(idSala, userId, userType);

            var sala = await _chatService.GetRoomByIdAsync(idSala);
            int otroId = userType == "Donante" ? sala.IdInstitucion : sala.IdUsuarioDonante;
            string otroTipo = userType == "Donante" ? "Institucion" : "Donante";
            string otroNombre = await _chatService.GetParticipantNameAsync(otroId, otroTipo);
            string necesidadInfo = await _chatService.GetNecesidadInfoAsync(sala.IdPostNecesidad);

            ViewBag.IdSala = idSala;
            ViewBag.CurrentUserType = userType;
            ViewBag.CurrentUserId = userId;
            ViewBag.OtroNombre = otroNombre;
            ViewBag.NecesidadInfo = necesidadInfo;

            return View("Chat", historial);
        }

        [HttpPost]
        public async Task<IActionResult> Start(int idInstitucion, int? idPostNecesidad = null)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userTypeClaim = User.FindFirstValue(ClaimTypes.Role); 

           
            if (userIdClaim == null || userTypeClaim == null) return RedirectToAction("login", "Account",new { area ="Donante"}); // regirige al login si no esta autenticando
            
            if (userTypeClaim != "Donante") return Forbid();

            int idDonante = int.Parse(userIdClaim);
            var sala = await _chatService.GetOrCreateRoomAsync(idDonante, idInstitucion, idPostNecesidad);
          
                return RedirectToAction(nameof(Index), new { idSala = sala.Id });
           
            

        }

        [HttpGet]
        public async Task<IActionResult> GetConversationsList()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var userType = User.FindFirstValue(ClaimTypes.Role)!;

            var salas = await _chatService.GetUserRoomsAsync(userId, userType);
            var result = new List<object>();
            foreach (var s in salas)
            {
                int otroId = userType == "Donante" ? s.IdInstitucion : s.IdUsuarioDonante;
                string otroTipo = userType == "Donante" ? "Institucion" : "Donante";
                string otroNombre = await _chatService.GetParticipantNameAsync(otroId, otroTipo);
                var ultimo = s.Mensajes.OrderByDescending(m => m.FechaEnvio).FirstOrDefault();
                result.Add(new { IdSala = s.Id, OtroNombre = otroNombre, UltimoMensaje = ultimo?.Contenido });
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(int idSala, IFormFile image)
        {
            if (image == null || image.Length == 0) return BadRequest();

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/chat");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(stream);

            var imageUrl = $"/uploads/chat/{fileName}";
            return Ok(new { success = true, imageUrl });
        }
    }

    public class ChatListViewModel
    {
        public int IdSala { get; set; }
        public string OtroParticipanteNombre { get; set; } = string.Empty;
        public string? UltimoMensaje { get; set; }
        public DateTime? FechaUltimoMensaje { get; set; }
        public int? IdPostNecesidad { get; set; }
    }
}