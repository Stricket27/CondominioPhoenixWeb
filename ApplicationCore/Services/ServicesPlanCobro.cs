using Infraestruture.Models;
using Infraestruture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicesPlanCobro : IServicesPlanCobro
    {


        public PlanCobro Agregar(PlanCobro pPlanCobro, string[] seleccionRubroCobro)
        {
            IRepositoryPlanCobro repositoryPlanCobro = new RepositoryPlanCobro();
            return repositoryPlanCobro.Agregar(pPlanCobro, seleccionRubroCobro);
        }

        public void CambiarEstado(int id)
        {
            IRepositoryPlanCobro repositoryPlanCobro = new RepositoryPlanCobro();
            repositoryPlanCobro.CambiarEstado(id);
        }

        public PlanCobro Editar(PlanCobro pPlanCobro, string[] seleccionRubroCobro)
        {
            IRepositoryPlanCobro repositoryPlanCobro = new RepositoryPlanCobro();
            return repositoryPlanCobro.Editar(pPlanCobro, seleccionRubroCobro);
        }

        public IEnumerable<PlanCobro> GetPlanCobro()
        {
            IRepositoryPlanCobro repositoryPlanCobro = new RepositoryPlanCobro();
            return repositoryPlanCobro.GetPlanCobro();
        }

        public PlanCobro GetPlanCobroById(int id)
        {
            IRepositoryPlanCobro repositoryPlanCobro = new RepositoryPlanCobro();
            return repositoryPlanCobro.GetPlanCobroById(id);
        }

        public IEnumerable<PlanCobro> GetPlanCobroByRubroCobro(int IDRubroCobro)
        {
            IRepositoryPlanCobro repositoryPlanCobro = new RepositoryPlanCobro();
            return repositoryPlanCobro.GetPlanCobroByRubroCobro(IDRubroCobro);
        }

        public void GetPlanCobroOutCobro(out string etiquetas1, out string valores1)
        {
            IRepositoryPlanCobro repositoryPlanCobro = new RepositoryPlanCobro();
            repositoryPlanCobro.GetPlanCobroOutCobro(out string etiquetas, out string valores);
            etiquetas1 = etiquetas;
            valores1 = valores;
        }
    }
}
