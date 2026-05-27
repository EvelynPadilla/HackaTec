using HackaTec.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HackaTec.Services
{
    public class ChatService
    {
        private readonly HackatecContext _context;

        public ChatService(HackatecContext context)
        {
            _context = context;
        }

        // Obtener o crear sala entre donante e institución (opcionalmente vinculada a una necesidad)
        public async Task<SalasChat> GetOrCreateRoomAsync(int idUsuarioDonante, int idInstitucion, int? idPostNecesidad = null)
        {
            var sala = await _context.SalasChat
                .FirstOrDefaultAsync(s => s.IdUsuarioDonante == idUsuarioDonante && s.IdInstitucion == idInstitucion
                                          && (idPostNecesidad == null || s.IdPostNecesidad == idPostNecesidad));

            if (sala == null)
            {
                sala = new SalasChat
                {
                    IdUsuarioDonante = idUsuarioDonante,
                    IdInstitucion = idInstitucion,
                    IdPostNecesidad = idPostNecesidad,
                    FechaCreacion = DateTime.Now
                };
                _context.SalasChat.Add(sala);
                await _context.SaveChangesAsync();
            }
            return sala;
        }

        // Guardar mensaje
        public async Task<Mensajes> SaveMessageAsync(int idSala, string remitenteTipo, int idRemitente, string contenido, string? rutaImagen = null)
        {
            var mensaje = new Mensajes
            {
                IdSala = idSala,
                RemitenteTipo = remitenteTipo,
                IdRemitente = idRemitente,
                Contenido = contenido,
                RutaImagen = rutaImagen,
                EstadoLeido = false,
                FechaEnvio = DateTime.Now
            };
            _context.Mensajes.Add(mensaje);
            await _context.SaveChangesAsync();
            return mensaje;
        }

        // Obtener historial de mensajes de una sala
        public async Task<List<Mensajes>> GetChatHistoryAsync(int idSala)
        {
            return await _context.Mensajes
                .Where(m => m.IdSala == idSala)
                .OrderBy(m => m.FechaEnvio)
                .ToListAsync();
        }

        // Obtener todas las salas donde participa un usuario
        public async Task<List<SalasChat>> GetUserRoomsAsync(int userId, string userType)
        {
            IQueryable<SalasChat> query = _context.SalasChat
                .Include(s => s.IdInstitucionNavigation)
                .Include(s => s.IdUsuarioDonanteNavigation)
                .Include(s => s.IdPostNecesidadNavigation)
                .Include(s => s.Mensajes);

            if (userType == "Donante")
                query = query.Where(s => s.IdUsuarioDonante == userId);
            else // Institucion
                query = query.Where(s => s.IdInstitucion == userId);

            return await query.OrderByDescending(s => s.FechaCreacion).ToListAsync();
        }

        public async Task<bool> IsUserInRoomAsync(int idSala, int userId, string userType)
        {
            var sala = await _context.SalasChat.FindAsync(idSala);
            if (sala == null) return false;
            return userType == "Donante" ? sala.IdUsuarioDonante == userId : sala.IdInstitucion == userId;
        }

        public async Task MarkMessagesAsReadAsync(int idSala, int currentUserId, string currentUserType)
        {
            var mensajes = await _context.Mensajes
                .Where(m => m.IdSala == idSala && m.RemitenteTipo != currentUserType && m.IdRemitente != currentUserId && !m.EstadoLeido)
                .ToListAsync();
            foreach (var m in mensajes)
                m.EstadoLeido = true;
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetParticipantNameAsync(int id, string tipo)
        {
            if (tipo == "Donante")
            {
                var donante = await _context.Usuario.FindAsync(id);
                return donante?.Nombre ?? "Donante";
            }
            else
            {
                var institucion = await _context.InstitucionesEducativas.FindAsync(id);
                return institucion?.Nombre ?? "Institución";
            }
        }

        public async Task<SalasChat?> GetRoomByIdAsync(int idSala)
        {
            return await _context.SalasChat.FindAsync(idSala);
        }

        public async Task<string?> GetNecesidadInfoAsync(int? idPostNecesidad)
        {
            if (idPostNecesidad == null) return null;
            var necesidad = await _context.PublicacionesNecesidad.FindAsync(idPostNecesidad);
            return necesidad?.Titulo;
        }
    }
}
