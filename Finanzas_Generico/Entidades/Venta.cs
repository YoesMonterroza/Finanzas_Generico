using System;

namespace Finanzas_Generico.Entidades
{
    public struct Venta
    {
        public string codigo { get; set; }
        public string identCliente { get; set; }
        public string listaProductos { get; set; }
        public decimal subTotal { get; set; }
        public decimal descuento { get; set; }
        public decimal total { get; set; }
        public string tipoPago { get; set; }
        public decimal montoAbono { get; set; }
        public string observacion { get; set; }
        public int usuarioModifica { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaModificacion { get; set; }

        //public string Codigo()
        //{
        //    return codigo;
        //}

        //public void setCodigo(string codigo)
        //{
        //    this.codigo = codigo;
        //}

        //public string Identificacion()
        //{
        //    return identCliente;
        //}

        //public void setIdentificacion(string identificacion)
        //{
        //    this.identCliente = identificacion;
        //}

        //public string ListaProductos()
        //{
        //    return listaProductos;
        //}

        //public void setListaProductos(string listaProdcutos)
        //{
        //    this.listaProductos = listaProdcutos;
        //}

        //public decimal SubTotal()
        //{
        //    return subTotal;
        //}

        //public void setSubTotal(decimal subTotal)
        //{
        //    this.subTotal = subTotal;
        //}

        //public decimal Descuento()
        //{
        //    return descuento;
        //}

        //public void setDescuento(decimal descuento)
        //{
        //    this.descuento = descuento;
        //}

        //public decimal Total()
        //{
        //    return total;
        //}

        //public void setTotal(decimal total)
        //{
        //    this.total = total;
        //}

        //public string TipoPago()
        //{
        //    return tipoPago;
        //}

        //public void setTipoPago(string tipoPagp)
        //{
        //    this.tipoPago = tipoPagp;
        //}

        //public int UsuarioModifica()
        //{
        //    return usuarioModifica;
        //}

        //public void setUsuarioModifica(int usuarioModifica)
        //{
        //    this.usuarioModifica = usuarioModifica;
        //}

        //public DateTime FechaCreacion()
        //{
        //    return fechaCreacion;
        //}

        //public void setFechaCreacion(DateTime fechaCreacion)
        //{
        //    this.fechaCreacion = fechaCreacion;
        //}

        //public DateTime FechaModificacion()
        //{
        //    return fechaModificacion;
        //}

        //public void setFechaModificacion(DateTime fechaModificacion)
        //{
        //    this.fechaModificacion = fechaModificacion;
        //}

        //public decimal MontoAbono()
        //{
        //    return montoAbono;
        //}

        //public void setMontoAbono(decimal montoAbono)
        //{
        //    this.montoAbono = montoAbono;
        //}

        //public string Observacion()
        //{
        //    return observacion;
        //}

        //public void setObservacion(string observacion)
        //{
        //    this.observacion = observacion;
        //}
    }

    public struct TablaListaVenta
    {
        public string codigo { get; set; }
        public decimal subTotal { get; set; }
        public decimal descuento { get; set; }
        public decimal total { get; set; }
        public DateTime fechaCreacion { get; set; }
        
    }
}