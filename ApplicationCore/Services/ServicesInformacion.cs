using Infraestruture.Models;
using Infraestruture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicesInformacion : IServicesInformacion
    {
        public void Agregar(Informacion pInformacion)
        {
            IRepositoryInformacion repositoryInformacion = new RepositoryInformacion();
            repositoryInformacion.Agregar(pInformacion);
        }

        public void CambiarEstado(int id)
        {
            IRepositoryInformacion repositoryInformacion = new RepositoryInformacion();
            repositoryInformacion.CambiarEstado(id);
        }

        public Informacion Editar(Informacion pInformacion)
        {
            IRepositoryInformacion repositoryInformacion = new RepositoryInformacion();
            return repositoryInformacion.Editar(pInformacion);
        }

        public IEnumerable<Informacion> GetInformacion()
        {
            IRepositoryInformacion repositoryInformacion = new RepositoryInformacion();
            return repositoryInformacion.GetInformacion();
        }

        public Informacion GetInformacionById(int id)
        {
            IRepositoryInformacion repositoryInformacion = new RepositoryInformacion();
            return repositoryInformacion.GetInformacionById(id);
        }
    }
}
