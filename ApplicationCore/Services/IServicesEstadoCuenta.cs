using Infraestruture.Models.ViewModel;
using Infraestruture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServicesEstadoCuenta
    {
        IEnumerable<EstadoCuentaResidenciaPlanViewModel> GetEstadoCuenta();
        EstadoCuenta GetEstadoCuentaById(int id);
        void Agregar(EstadoCuenta pEstadoCuenta);
        EstadoCuenta Editar(EstadoCuenta pEstadoCuenta);
        void CambiarEstado(int id);
        List<Residencia> GetResidencia();
        List<PlanCobro> GetPlanCobro();
        IEnumerable<EstadoCuentaResidenciaPlanViewModel> GetEstadoCuentaByResidencia(string nombre);

    }
}
