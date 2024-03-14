using Infraestruture.Models;
using Infraestruture.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServicesReservacion
    {
        IEnumerable<ReservacionUsuarioAreaViewModel> GetReservacion();
        List<ReservacionUsuarioAreaViewModel> GetReservacionesById(int id);
        void Agregar(Reservacion pReservacion);
        //Reservacion Editar(Reservacion pReservacion);
        //void CambiarEstado(int id);
        void CambiarConfirmacion(int id);
        List<AreaComun> GetAreaComun();
        Usuario GetUsuarioById(int id);
        void EnviarCorreo();
    }
}
