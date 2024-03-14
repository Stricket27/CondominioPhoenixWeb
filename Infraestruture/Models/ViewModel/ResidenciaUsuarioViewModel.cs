using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestruture.Models.ViewModel
{
    public class ResidenciaUsuarioViewModel
    {
        //USUARIO
        public string NombreCompleto { get; set; }

        //RESIDENCIA
        public string ID_Residencia { get; set; }
        public string NumeroResidencia { get; set; }
        public string CantidadPersonas { get; set; }
        public string AnnoInicio { get; set; }
        public string CantidadAutos { get; set; }
        public string EstadoResidencia { get; set; }
        public string EstadoActual { get; set; }
    }
}
