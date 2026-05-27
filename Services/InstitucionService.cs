using HackaTec.Areas.Admin.ViewModels;
using HackaTec.Areas.Institucion.ViewModels;
using HackaTec.Models.Entities;
using HackaTec.Repositories;


namespace HackaTec.Services
{
    public class InstitucionService
    {
        private readonly Repository<InstitucionesEducativas> repoEscuelas;
        private readonly Repository<PublicacionesNecesidad> repoPublicaciones;
        public InstitucionService(Repository<InstitucionesEducativas> repoEscuelas, Repository<PublicacionesNecesidad> repoPublicaciones)
        {
            this.repoEscuelas = repoEscuelas;
            this.repoPublicaciones = repoPublicaciones;
        }
        //public InstitucionesEducativas? Login(LoginInstitucionesViewModel model)
        //{
        //    bool ingresoCorrreo = model.Correo_CCT.Contains("@");
            
        //    if (ingresoCorrreo)
        //    {
        //        var institucion = repoEscuelas.GetAll().FirstOrDefault(i =>
        //        i. == model.Correo_CCT);
        //        return institucion;
        //    }
        //    else
        //    {
        //        var institucion = repoEscuelas.GetAll().FirstOrDefault(i =>
        //        i.Cct == model.Correo_CCT);
        //        return institucion;
        //    }
           
        //}
    }
}
