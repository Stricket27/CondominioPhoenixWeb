using Infraestruture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServicesRubroCobro
    {
        IEnumerable<RubroCobro> GetRubroCobro();
        RubroCobro GetRubroCobroById(int id);
        void Agregar(RubroCobro pRubroCobro);
        RubroCobro Editar(RubroCobro pRubroCobro);
        void CambiarEstado(int id);
        IEnumerable<RubroCobro> GetRubroCobrosActivo();
    }
}
