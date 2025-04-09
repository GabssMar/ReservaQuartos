using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaQuarto
{
    public class Reserva
    {
        public Quarto Quarto { get; set; }
        public string NomeHospede { get; set; }
        public string Documento { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
        public decimal CustoTotal { get; set; }
    }
}
