using Infraestruture.Models;
using Infraestruture.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestruture.Repository
{
    public interface IRepositoryResidencia
    {
        IEnumerable<ResidenciaUsuarioViewModel> GetResidencia();
        Residencia GetResidenciaById(int id);
        void Agregar(Residencia pResidencia);
        Residencia Editar(Residencia pResidencia);
        void CambiarEstado(int id);
        List<Usuario> GetUsuario();

    }
}
