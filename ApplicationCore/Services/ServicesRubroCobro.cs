using Infraestruture.Models;
using Infraestruture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicesRubroCobro : IServicesRubroCobro
    {
        public void Agregar(RubroCobro pRubroCobro)
        {
            IRepositoryRubroCobro repositoryRubroCobro = new RepositoryRubroCobro();
            repositoryRubroCobro.Agregar(pRubroCobro);
        }

        public void CambiarEstado(int id)
        {
            IRepositoryRubroCobro repositoryRubroCobro = new RepositoryRubroCobro();
            repositoryRubroCobro.CambiarEstado(id);
        }

        public RubroCobro Editar(RubroCobro pRubroCobro)
        {
            IRepositoryRubroCobro repositoryRubroCobro = new RepositoryRubroCobro();
            return repositoryRubroCobro.Editar(pRubroCobro);
        }

        public IEnumerable<RubroCobro> GetRubroCobro()
        {
            IRepositoryRubroCobro repositoryRubroCobro = new RepositoryRubroCobro();
            return repositoryRubroCobro.GetRubroCobro();
        }

        public RubroCobro GetRubroCobroById(int id)
        {
            IRepositoryRubroCobro repositoryRubroCobro = new RepositoryRubroCobro();
            return repositoryRubroCobro.GetRubroCobroById(id);
        }

        public IEnumerable<RubroCobro> GetRubroCobrosActivo()
        {
            IRepositoryRubroCobro repositoryRubroCobro = new RepositoryRubroCobro();
            return repositoryRubroCobro.GetRubroCobrosActivo();
        }
    }
}
