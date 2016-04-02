using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzas_Generico.Entidades
{
    public class Venta
    {
        private string codigo { get; set; }
        private string identCliente { get; set; }
        private string listaProductos { get; set; }
        private decimal subTotal { get; set; }
        private decimal descuento { get; set; }
        private decimal total { get; set; }
        private string tipoPago { get; set; }
        private int usuarioModifica { get; set; }
        private DateTime fechaCreacion { get; set; }
        private DateTime fechaModificacion { get; set; }

        public string Codigo() {
            return codigo;
        }

        public void setCodigo(string codigo)
        {
            this.codigo = codigo;
        }

        public string Identificacion()
        {
            return identCliente;
        }

        public void setIdentificacion(string identificacion)
        {
            this.identCliente = identificacion;
        }

        public string ListaProductos()
        {
            return listaProductos;
        }

        public void setListaProductos(string listaProdcutos)
        {
            this.listaProductos = listaProdcutos;
        }

        public decimal SubTotal()
        {
            return subTotal;
        }

        public void setSubTotal(decimal subTotal)
        {
            this.subTotal = subTotal;
        }

        public decimal Descuento()
        {
            return descuento;
        }

        public void setDescuento(decimal descuento)
        {
            this.descuento = descuento;
        }

        public decimal Total()
        {
            return total;
        }

        public void setTotal(decimal total)
        {
            this.total = total;
        }

        public string TipoPago()
        {
            return tipoPago;
        }

        public void setTipoPago(string tipoPagp)
        {
            this.tipoPago = tipoPagp;
        }

        public int UsuarioModifica()
        {
            return usuarioModifica;
        }

        public void setUsuarioModifica(int usuarioModifica)
        {
            this.usuarioModifica = usuarioModifica;
        }

        public DateTime FechaCreacion()
        {
            return fechaCreacion;
        }

        public void setFechaCreacion(DateTime fechaCreacion)
        {
            this.fechaCreacion = fechaCreacion;
        }

        public DateTime FechaModificacion()
        {
            return fechaModificacion;
        }

        public void setFechaModificacion(DateTime fechaModificacion)
        {
            this.fechaModificacion = fechaModificacion;
        }
    }
}
