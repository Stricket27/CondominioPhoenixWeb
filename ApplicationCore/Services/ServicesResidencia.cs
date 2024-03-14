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
    public class ServicesResidencia : IServicesResidencia
    {
        public void Agregar(Residencia pResidencia)
        {
            IRepositoryResidencia repositoryResidencia = new RepositoryResidencia();
            repositoryResidencia.Agregar(pResidencia);
        }

        public void CambiarEstado(int id)
        {
            IRepositoryResidencia repositoryResidencia = new RepositoryResidencia();
            repositoryResidencia.CambiarEstado(id);
        }

        public Residencia Editar(Residencia pResidencia)
        {
            IRepositoryResidencia repositoryResidencia = new RepositoryResidencia();
            return repositoryResidencia.Editar(pResidencia);
        }

        public IEnumerable<ResidenciaUsuarioViewModel> GetResidencia()
        {
            IRepositoryResidencia repositoryResidencia = new RepositoryResidencia();
            return repositoryResidencia.GetResidencia();
        }

        public Residencia GetResidenciaById(int id)
        {
            IRepositoryResidencia repositoryResidencia = new RepositoryResidencia();
            return repositoryResidencia.GetResidenciaById(id);
        }

        public List<Usuario> GetUsuario()
        {
            IRepositoryResidencia repositoryResidencia = new RepositoryResidencia();
            return repositoryResidencia.GetUsuario();
        }
    }
}
