using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestruture.Models.ViewModel
{
    public class EstadoCuentaResidenciaPlanViewModel
    {
        //ESTADO CUENTA
        public string ID_EstadoCuenta { get; set; }
        public string EstadoPago { get; set; }
        public string ProximaFechaPago { get; set; }
        public string EstadoActual { get; set; }

        //RESIDENCIA
        public string NumeroResidencia { get; set; }

        //PLAN
        public string DescripcionPlan { get; set; }

    }
}
