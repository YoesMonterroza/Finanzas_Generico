using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzas_Generico.Entidades
{
    public struct Producto
    {
        public String id { get; set; }
        public String codigo { get; set; }
        public String nombre { get; set; }
        public Int32 cantidad { get; set; }
        public Int32 cantidadMinima { get; set; }
        public Decimal precio { get; set; }
        public String estado { get; set; }
        public String descripcion { get; set; }
        public DateTime fechaIngreso { get; set; }
        public DateTime fechaModificacion { get; set; }
        public Int32 usuarioModifica { get; set; }

        //public Producto() { }

        //public Producto(String codigo, String nombre, Int32 cantidad, Int32 cantidadMinima, Decimal precio, String descripcion)
        //{
        //    this.codigo = codigo;
        //    this.nombre = nombre;
        //    this.cantidad = cantidad;
        //    this.cantidadMinima = cantidadMinima;
        //    this.precio = precio;
        //    this.descripcion = descripcion;
        //}
    }

    public struct ListaProducto
    {
        public String codigo { get; set; }
        public String nombre { get; set; }
        public Int32 cantidad { get; set; }
        public Int32 cantidadMinima { get; set; }
        public Decimal precio { get; set; }
        public String descripcion { get; set; }
    }

    public struct ListaProductoFactura
    {
        public String codigo { get; set; }
        public String nombre { get; set; }
        public Int32 cantidad { get; set; }
        public Decimal precio { get; set; }
    }

    public struct ListaProductoVenta
    {
        public String codigo { get; set; }
        public String nombre { get; set; }
        public Int32 cantidad { get; set; }
        public Decimal precio { get; set; }
        public Decimal precioTotal { get; set; }
    }
}
