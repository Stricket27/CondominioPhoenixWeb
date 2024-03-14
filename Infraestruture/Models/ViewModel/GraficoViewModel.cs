using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestruture.Models.ViewModel
{
    public class GraficoViewModel
    {
        public string titulo { get; set; }
        public string tituloEtiquetas { get; set; }
        public string tipo { get; set; }
        public string Etiquetas { get; set; }
        public string Valores { get; set; }
        public string Colores { get; set; }

        public List<string> GenerateColors(int pCantidad)
        {
            var colors = new List<string>();
            var random = new Random();
            for (int i = 0; i < pCantidad; i++)
            {
                colors.Add(String.Format("#{0:X6}", random.Next(0x1000000)));
            }
            return colors;
        }
    }
}
