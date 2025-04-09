using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaQuarto
{
    public class Quarto
    {
        public int Numero { get; set; }
        public TipoQuarto Tipo { get; set; }
        public decimal PrecoPorNoite { get; set; }
        public StatusQuarto Status { get; set; }
    }
}
