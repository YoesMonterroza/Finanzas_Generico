using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzas_Generico.Entidades
{
    public class Producto
    {
        public String id { get; set; }
        public String codigo { get; set; }
        public String nombre { get; set; }
        public Int32 cantidad { get; set; }
        public Int32 cantidadMinima { get; set; }
        public Decimal precio { get; set; }
        public DateTime fechaIngreso { get; set; }
        public DateTime fechaModificacion { get; set; }
        public Int32 usuarioModifica { get; set; }
    }
}
