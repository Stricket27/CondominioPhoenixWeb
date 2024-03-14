using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestruture.Models.ViewModel
{
    public class IncidenciaUsuarioViewModel
    {
        //USUARIO
        public string NombreCompleto { get; set; }

        //INCIDENCIA
        public string ID_Incidencias { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string FechaIncidencia { get; set; }
        public string EstadoSolicitud { get; set; }
        public string EstadoActual { get; set; }
    }
}
