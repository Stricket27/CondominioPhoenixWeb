using Infraestruture.Models;
using Infraestruture.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestruture.Repository
{
    public interface IRepositoryReservacion
    {
        //INDEX
        IEnumerable<ReservacionUsuarioAreaViewModel> GetReservacion();
        List<ReservacionUsuarioAreaViewModel> GetReservacionesById (int id);
        void Agregar(Reservacion pReservacion);
        //void CambiarEstado(int id);
        void CambiarConfirmacion (int id);
        //AREA COMUN
        List<AreaComun> GetAreaComun();
        Usuario GetUsuarioById(int id);
        void EnviarCorreo();

    }
}
