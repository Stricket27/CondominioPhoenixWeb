using Infraestruture.Models;
using Infraestruture.Models.ViewModel;
using Infraestruture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicesReservacion : IServicesReservacion
    {
        public void Agregar(Reservacion pReservacion)
        {
            IRepositoryReservacion repositoryReservacion = new RepositoryReservacion();
            repositoryReservacion.Agregar(pReservacion);
        }

        public void CambiarConfirmacion(int id)
        {
            IRepositoryReservacion repositoryReservacion = new RepositoryReservacion();
            repositoryReservacion.CambiarConfirmacion(id);
        }

        public void EnviarCorreo()
        {
            IRepositoryReservacion repositoryReservacion = new RepositoryReservacion();
            repositoryReservacion .EnviarCorreo();
        }

        //public void CambiarEstado(int id)
        //{
        //    IRepositoryReservacion repositoryReservacion = new RepositoryReservacion();
        //    repositoryReservacion.CambiarEstado(id);
        //}

        public List<AreaComun> GetAreaComun()
        {
            IRepositoryReservacion repositoryReservacion = new RepositoryReservacion();
            return repositoryReservacion.GetAreaComun();
        }

        public IEnumerable<ReservacionUsuarioAreaViewModel> GetReservacion()
        {
            IRepositoryReservacion repositoryReservacion = new RepositoryReservacion();
            return repositoryReservacion.GetReservacion();
        }

        public List<ReservacionUsuarioAreaViewModel> GetReservacionesById(int id)
        {
            IRepositoryReservacion repositoryReservacion = new RepositoryReservacion();
            return repositoryReservacion.GetReservacionesById(id);
        }

        public Usuario GetUsuarioById(int id)
        {
            IRepositoryReservacion repositoryReservacion = new RepositoryReservacion();
            return repositoryReservacion.GetUsuarioById(id);
        }
    }
}
