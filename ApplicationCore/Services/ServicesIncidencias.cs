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
    public class ServicesIncidencias : IServicesIncidencias
    {
        public void Agregar(Incidencias pIncidencias)
        {
            IRepositoryIncidencias repositoryIncidencias = new RepositoryIncidencias();
            repositoryIncidencias.Agregar(pIncidencias);
        }

        public void CambiarEstado(int id)
        {
            IRepositoryIncidencias repositoryIncidencias = new RepositoryIncidencias();
            repositoryIncidencias.CambiarEstado(id);
        }

        public Incidencias Editar(Incidencias pIncidencias)
        {
            IRepositoryIncidencias repositoryIncidencias = new RepositoryIncidencias();
            return repositoryIncidencias.Editar(pIncidencias);
        }

        public IEnumerable<IncidenciaUsuarioViewModel> GetIncidencias()
        {
            IRepositoryIncidencias repositoryIncidencias = new RepositoryIncidencias();
            return repositoryIncidencias.GetIncidencias();
        }

        public Incidencias GetIncidenciasById(int id)
        {
            IRepositoryIncidencias repositoryIncidencias = new RepositoryIncidencias();
            return repositoryIncidencias.GetIncidenciasById(id);
        }

        public List<Usuario> GetUsuario()
        {
            IRepositoryIncidencias repositoryIncidencias = new RepositoryIncidencias();
            return repositoryIncidencias.GetUsuario();
        }
    }
}
