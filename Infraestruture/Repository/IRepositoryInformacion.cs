using Infraestruture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestruture.Repository
{
    public interface IRepositoryInformacion
    {
        IEnumerable<Informacion> GetInformacion();
        Informacion GetInformacionById(int id);
        void Agregar(Informacion pInformacion);
        Informacion Editar(Informacion pInformacion);
        void CambiarEstado(int id);

    }
}
