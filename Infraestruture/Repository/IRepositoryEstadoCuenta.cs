using Infraestruture.Models.ViewModel;
using Infraestruture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestruture.Repository
{
    public interface IRepositoryEstadoCuenta
    {
        IEnumerable<EstadoCuentaResidenciaPlanViewModel> GetEstadoCuenta();
        EstadoCuenta GetEstadoCuentaById(int id);
        void Agregar(EstadoCuenta pEstadoCuenta);
        EstadoCuenta Editar(EstadoCuenta pEstadoCuenta);
        void CambiarEstado(int id);
        List<Residencia> GetResidencia();
        List<PlanCobro> GetPlanCobro();
        //BUSQUEDA
        IEnumerable<EstadoCuentaResidenciaPlanViewModel> GetEstadoCuentaByResidencia(string nombre);
    }
}
