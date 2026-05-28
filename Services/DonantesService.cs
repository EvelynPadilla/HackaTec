using HackaTec.Areas.Donante.ViewModels;
using HackaTec.Areas.Institucion.ViewModels;
using HackaTec.Models.Entities;
using HackaTec.Repositories;

namespace HackaTec.Services
{
    public class DonantesService
    {
        private readonly Repository<Usuario> repository;

        public DonantesService(Repository<Usuario> repository)
        {
            this.repository = repository;
        }

        public Usuario? Autenticar(LoginDonanteViewModel vm)
        {
            var passwordHash = Sha256Helper.ComputeHash(vm.ContrasenaHash);
            var profesor = repository.GetAll().FirstOrDefault(x => x.Correo == vm.Correo && x.ContrasenaHash == passwordHash);
            return profesor;
        }

        public (bool, string) Registrar(RegistroDonanteViewModel vm)
        {
            string Mensaje = "";
            if (repository.GetAll().Any(x => x.Correo == vm.Correo))
            {
                Mensaje += "El correo que intentas ingresar ya esta registrado";
            }

            if (Mensaje != "")
            {
                return (false, Mensaje);
            }
            Usuario nuevo = new Usuario()
            {
                ContrasenaHash = Sha256Helper.ComputeHash(vm.ContrasenaHash ?? ""),//Cotraseña cifrada para guardarse en la base de datos
                Correo = vm.Correo,
                Nombre = vm.Nombre,
                Apellidos = vm.Apellidos,
                Telefono = vm.Telefono,
                Rol = "Donante",
                FotoPerfil = vm.FotoPerfil,
                PublicacionesAgradecimiento = new List<PublicacionesAgradecimiento>(),
                SalasChat = new List<SalasChat>(),
            };
            repository.Insert(nuevo);
            return (true, "");
        }

        public PerfilViewModel ObtenerPerfil(int idUsuario)
        {
            var usuario = repository.Get(idUsuario);
            if (usuario == null) return null;
            return new PerfilViewModel
            {
                Nombre = usuario.Nombre,
                Apellidos = usuario.Apellidos,
                Rol = usuario.Rol,
                FotoPerfil = usuario.FotoPerfil,
                Publicaciones = usuario.PublicacionesAgradecimiento.Select(pa => new PublicacionesAgradecimientoViewModel
                {
                    Id = pa.Id,
                    IdPublicacionNecesidad = pa.IdPublicacionNecesidad,
                    IdUsuarioDonante = pa.IdUsuarioDonante,
                    Descripcion = pa.Descripcion,
                    RutaFotografia = pa.RutaFotografia,
                    Fecha = pa.Fecha
                }).ToList()
            };


        }
    }
}
