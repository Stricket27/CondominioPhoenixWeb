using Infraestruture.Models;
using Infraestruture.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestruture.Repository
{
    public interface IRepositoryPlanCobro
    {
        IEnumerable<PlanCobro> GetPlanCobro();
        IEnumerable<PlanCobro> GetPlanCobroByRubroCobro(int IDRubroCobro);
        PlanCobro GetPlanCobroById(int id);
        PlanCobro Agregar(PlanCobro pPlanCobro, string[] seleccionRubroCobro);
        PlanCobro Editar(PlanCobro pPlanCobro, string[] seleccionRubroCobro);
        void CambiarEstado(int id);
        void GetPlanCobroOutCobro(out string etiquetas, out string valores);

    }
}
