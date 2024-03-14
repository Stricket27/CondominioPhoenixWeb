using Infraestruture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServicesAreaComun
    {
        IEnumerable<AreaComun> GetAreaComun();
        void Agregar(AreaComun pAreaComun);
        AreaComun GetAreaComunById(int id);
        void CambiarEstado(int id);
        AreaComun Editar(AreaComun pAreaComun);
    }
}
