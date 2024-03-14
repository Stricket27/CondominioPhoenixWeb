using Infraestruture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestruture.Repository
{
    public interface IRepositoryAreaComun
    {
        IEnumerable<AreaComun> GetAreaComun();
        void Agregar(AreaComun pAreaComun);
        AreaComun GetAreaComunById(int id);
        void CambiarEstado(int id);
        AreaComun Editar(AreaComun pAreaComun);
    }
}
