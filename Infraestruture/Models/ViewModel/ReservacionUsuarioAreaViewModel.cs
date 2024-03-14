using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestruture.Models.ViewModel
{
    public class ReservacionUsuarioAreaViewModel
    {
        //USUARIO
        public string NombreCompleto { get; set; }
        //AREA COMUN
        public string NombreAreaComun { get; set; }
        //RESERVACION
        public string ID_Reservacion { get; set; }
        public string Confirmacion { get; set; }
        public string Dia { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFinal { get; set; }
    }
}
