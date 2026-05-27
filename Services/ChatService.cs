using HackaTec.Models.Entities;

namespace HackaTec.Services
{
    public class ChatService
    {
        private readonly HackatecContext _context;

        public ChatService(HackatecContext context)
        {
            _context = context;
        }

        public async Task<SalasChat> GetOrCreateRoomAsync(int participante1Id, int participante2Id, string tipoParticipante1, string tipoParticipante2)
        {
            var sala = await _context.SalasChat
                .FirstOrDefaultAsync(s =>
                    (s.Participante1Id == participante1Id && s.Participante2Id == participante2Id) ||
                    (s.Participante1Id == participante2Id && s.Participante2Id == participante1Id));

            if (sala == null)
            {
                sala = new SalasChat
                {
                    Participante1Id = participante1Id,
                    Participante2Id = participante2Id,
                    TipoParticipante1 = tipoParticipante1,
                    TipoParticipante2 = tipoParticipante2,
                    FechaCreacion = DateTime.Now
                };
                _context.SalasChat.Add(sala);
                await _context.SaveChangesAsync();
            }
            return sala;
        }

        // Guarda un mensaje en la base de datos
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

        // Obtiene el historial de mensajes de una sala
        public async Task<List<Mensajes>> GetChatHistoryAsync(int idSala, int pageSize = 50)
        {
            return await _context.Mensajes
                .Where(m => m.IdSala == idSala)
                .OrderBy(m => m.FechaEnvio)
                .Take(pageSize)
                .ToListAsync();
        }

        // Marcar mensajes como leídos
        public async Task MarkMessagesAsReadAsync(int idSala, int currentUserId, string currentUserType)
        {
            var mensajes = await _context.Mensajes
                .Where(m => m.IdSala == idSala && m.RemitenteTipo != currentUserType && m.IdRemitente != currentUserId && !m.EstadoLeido)
                .ToListAsync();
            foreach (var m in mensajes)
                m.EstadoLeido = true;
            await _context.SaveChangesAsync();
        }
    }
}
