using Infraestruture.Models;
using Infraestruture.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestruture.Repository
{
    public interface IRepositoryIncidencias
    {
        IEnumerable<IncidenciaUsuarioViewModel> GetIncidencias();
        Incidencias GetIncidenciasById(int id);
        void Agregar(Incidencias pIncidencias);
        Incidencias Editar(Incidencias pIncidencias);
        List<Usuario> GetUsuario();
        void CambiarEstado(int id);
    }
}
