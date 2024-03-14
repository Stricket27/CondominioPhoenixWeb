using Infraestruture.Models;
using Infraestruture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicesAreaComun : IServicesAreaComun
    {
        public void Agregar(AreaComun pAreaComun)
        {
            IRepositoryAreaComun repositoryAreaComun = new RepositoryAreaComun();
            repositoryAreaComun.Agregar(pAreaComun);
        }

        public void CambiarEstado(int id)
        {
            IRepositoryAreaComun repositoryAreaComun = new RepositoryAreaComun();
            repositoryAreaComun.CambiarEstado(id);
        }

        public AreaComun Editar(AreaComun pAreaComun)
        {
            IRepositoryAreaComun repositoryAreaComun = new RepositoryAreaComun();
            return repositoryAreaComun.Editar(pAreaComun);
        }

        public IEnumerable<AreaComun> GetAreaComun()
        {
            IRepositoryAreaComun repositoryAreaComun = new RepositoryAreaComun();
            return repositoryAreaComun.GetAreaComun();
        }

        public AreaComun GetAreaComunById(int id)
        {
            IRepositoryAreaComun repositoryAreaComun = new RepositoryAreaComun();
            return repositoryAreaComun.GetAreaComunById(id);
        }
    }
}
