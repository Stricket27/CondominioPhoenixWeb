using Infraestruture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServicesInformacion
    {
        IEnumerable<Informacion> GetInformacion();
        Informacion GetInformacionById(int id);
        void Agregar(Informacion pInformacion);
        Informacion Editar(Informacion pInformacion);
        void CambiarEstado(int id);
    }
}
