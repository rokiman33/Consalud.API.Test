using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consalud.Model.Dto
{
    public class FacturasByComunasDto
    {
        public double ComunaId { get; set; }
        public List<FacturasDTO> Facturas { get; set; }
    }
}
