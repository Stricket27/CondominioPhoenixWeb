using Infraestruture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestruture.Repository
{
    public interface IRepositoryRubroCobro
    {
        IEnumerable<RubroCobro> GetRubroCobro();
        RubroCobro GetRubroCobroById(int id);
        void Agregar(RubroCobro pRubroCobro);
        RubroCobro Editar(RubroCobro pRubroCobro);
        void CambiarEstado(int id);
        IEnumerable<RubroCobro> GetRubroCobrosActivo();
    }
}
