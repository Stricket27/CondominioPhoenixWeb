using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestruture.Models.ViewModel
{
    public class PlanRubroCobroViewModel
    {
        //PLAN
        public string ID_PlanCobro { get; set; }
        public string DescripcionPlan { get; set; }
        public string EstadoActual { get; set; }

        //RUBRO
        public string ID_RubroCobro { get; set; }
        public string DescripcionRubro { get; set; }
        public string Precio { get; set; }
    }
}
