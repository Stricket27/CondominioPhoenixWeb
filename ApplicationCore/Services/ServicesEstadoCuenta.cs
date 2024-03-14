using Infraestruture.Models;
using Infraestruture.Models.ViewModel;
using Infraestruture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicesEstadoCuenta : IServicesEstadoCuenta
    {
        public void Agregar(EstadoCuenta pEstadoCuenta)
        {
            IRepositoryEstadoCuenta repositoryEstadoCuenta = new RepositoryEstadoCuenta();
            repositoryEstadoCuenta.Agregar(pEstadoCuenta);
        }

        public void CambiarEstado(int id)
        {
            IRepositoryEstadoCuenta repositoryEstadoCuenta = new RepositoryEstadoCuenta();
            repositoryEstadoCuenta.CambiarEstado(id);
        }

        public EstadoCuenta Editar(EstadoCuenta pEstadoCuenta)
        {
            IRepositoryEstadoCuenta repositoryEstadoCuenta = new RepositoryEstadoCuenta();
            return repositoryEstadoCuenta.Editar(pEstadoCuenta);
        }

        public IEnumerable<EstadoCuentaResidenciaPlanViewModel> GetEstadoCuenta()
        {
            IRepositoryEstadoCuenta repositoryEstadoCuenta = new RepositoryEstadoCuenta();
            return repositoryEstadoCuenta.GetEstadoCuenta();
        }

        

        public EstadoCuenta GetEstadoCuentaById(int id)
        {
            IRepositoryEstadoCuenta repositoryEstadoCuenta = new RepositoryEstadoCuenta();
            return repositoryEstadoCuenta.GetEstadoCuentaById(id);
        }

        public IEnumerable<EstadoCuentaResidenciaPlanViewModel> GetEstadoCuentaByResidencia(string nombre)
        {
            IRepositoryEstadoCuenta repositoryEstadoCuenta = new RepositoryEstadoCuenta();
            return repositoryEstadoCuenta.GetEstadoCuentaByResidencia(nombre);
        }

        public List<PlanCobro> GetPlanCobro()
        {
            IRepositoryEstadoCuenta repositoryEstadoCuenta = new RepositoryEstadoCuenta();
            return repositoryEstadoCuenta.GetPlanCobro();
        }

        public List<Residencia> GetResidencia()
        {
            IRepositoryEstadoCuenta repositoryEstadoCuenta = new RepositoryEstadoCuenta();
            return repositoryEstadoCuenta.GetResidencia();
        }
    }
}
