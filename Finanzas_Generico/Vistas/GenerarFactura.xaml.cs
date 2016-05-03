using Finanzas_Generico.Entidades;
using Finanzas_Generico.Manager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Finanzas_Generico.Vistas
{
    /// <summary>
    /// Interaction logic for GenerarFactura.xaml
    /// </summary>
    public partial class GenerarFactura : Window
    {
        private String[] captura;
        private ICollectionView IcvProductos;
        private ICollectionView IcvProductosVenta;
        private List<ListaProductoFactura> lProfact = new List<ListaProductoFactura>();
        private List<ListaProductoVenta> lProVent = new List<ListaProductoVenta>();
        private Persona per = new Persona();

        public GenerarFactura()
        {
            InitializeComponent();
            this.Title = string.Format(ConfigurationManager.AppSettings["formatoTitulos"].ToString(), Conexion.Utilidades.Usuario, "Registrar venta");
            ObtenerCodigoFactura();
            LLenarTabla();
            LLenarTablaVenta();
        }

        private void ObtenerCodigoFactura()
        {
            AdministrarVenta av = new AdministrarVenta();
            captura = av.ObtenerConsecutivo();
            lbl_nrofactura.Content = "Codigo de factura: " + captura[1];
        }

        public void LLenarTabla()
        {
            lProfact.Clear();
            lProfact = AdministrarProducto.ListaProductosFactura();
            IcvProductos = CollectionViewSource.GetDefaultView(lProfact);

            if (IcvProductos != null)
            {
                dg_ListaProductos.AutoGenerateColumns = true;
                dg_ListaProductos.IsReadOnly = true;
                dg_ListaProductos.MinColumnWidth = 126;
                dg_ListaProductos.MaxColumnWidth = 300;
                dg_ListaProductos.ItemsSource = IcvProductos;
            }
        }

        private void cb_TipoPago_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_TipoPago.SelectedIndex == 1)
            {
                lbl_Valor_Abono.Visibility = Visibility.Visible;
                txt_ValorAbono.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_Valor_Abono.Visibility = Visibility.Hidden;
                txt_ValorAbono.Visibility = Visibility.Hidden;
            }
        }

        private void txt_Descuento_TextChanged(object sender, TextChangedEventArgs e)
        {
            int descuento = 0;
            int Sub_total = 0;
            int total = 0;

            int.TryParse(txt_Descuento.Text, out descuento);
            int.TryParse(txt_SubTotal.Text, out Sub_total);

            if (Sub_total > 0 && descuento > 0)
            {
                total = Sub_total - descuento;
                txt_Total.Text = total.ToString();
            }
            else
            {
                txt_Total.Text = txt_SubTotal.Text;
            }
        }

        private void btn_BuscarCliente_Click(object sender, RoutedEventArgs e)
        {
            AdministrarPersona ap = new AdministrarPersona();
            per = ap.ConsultarPersona(txt_Identificacion.Text);

            txt_Nombre.Text = per.nombres + " " + per.apellidos;
            txt_Direccion.Text = per.direccion;
            txt_Telefono.Text = per.telefono;

            if (per.nombres != null)
            {
                per.identificacion = txt_Identificacion.Text; 
                txt_Identificacion.IsEnabled = false;
                btn_refrescar.IsEnabled = true;
            }
            else {
                MessageBox.Show("El cliente no existe debe registrarlo para continuar!");
            }
        }

        private void txt_Identificacion_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_Identificacion.Text == "")
            {
                txt_Nombre.Text = "";
                txt_Direccion.Text = "";
                txt_Telefono.Text = "";
            }
        }

        private void btn_refrescar_Click(object sender, RoutedEventArgs e)
        {
            txt_Identificacion.Text = "";
            txt_Nombre.Text = "";
            txt_Direccion.Text = "";
            txt_Telefono.Text = "";

            txt_Identificacion.IsEnabled = true;
            txt_Identificacion.Focus();
            btn_refrescar.IsEnabled = false;
        }

        private void txt_FiltrarNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            ICollectionView IcvProductosFiltro;
            List<ListaProductoFactura> listPro = new List<ListaProductoFactura>();
            ListaProductoFactura lp = new ListaProductoFactura();

            if (txt_FiltrarNombre.Text != "" && txt_FiltrarNombre.Text.Length > 2)
            {
                var filtro = from pro in AdministrarProducto.ListaProductosFactura()
                             where pro.nombre.Contains(txt_FiltrarNombre.Text)
                             select new { pro.codigo, pro.nombre, pro.cantidad, pro.precio };

                foreach (var fil in filtro)
                {
                    lp.codigo = fil.codigo;
                    lp.nombre = fil.nombre;
                    lp.precio = fil.precio;
                    lp.cantidad = fil.cantidad;
                    listPro.Add(lp);
                }

                IcvProductosFiltro = CollectionViewSource.GetDefaultView(listPro);

                if (IcvProductosFiltro != null)
                {
                    dg_ListaProductos.AutoGenerateColumns = true;
                    dg_ListaProductos.IsReadOnly = true;
                    dg_ListaProductos.MinColumnWidth = 126;
                    dg_ListaProductos.MaxColumnWidth = 300;
                    dg_ListaProductos.ItemsSource = IcvProductosFiltro;
                }
            }
            else
            {
                LLenarTabla();
            }
        }

        private void btn_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Agregar_Click(object sender, RoutedEventArgs e)
        {
            int capturarCantidad = 0;
            decimal capturaSTotal = 0;
            decimal sumaStotal = 0;
            int.TryParse(txt_CantidadProducto.Text, out capturarCantidad);
            decimal.TryParse(txt_SubTotal.Text, out capturaSTotal);

            ListaProductoVenta lpv = new ListaProductoVenta();
            if (dg_ListaProductos.SelectedIndex >= 0 && capturarCantidad > 0)
            {
                ListaProductoFactura lpf = new ListaProductoFactura();
                lpf = (ListaProductoFactura)dg_ListaProductos.SelectedValue;
                lpv.codigo = lpf.codigo;
                lpv.nombre = lpf.nombre;
                lpv.cantidad = int.Parse(txt_CantidadProducto.Text);
                lpv.precio = lpf.precio;
                lpv.precioTotal = (lpf.precio * int.Parse(txt_CantidadProducto.Text));

                sumaStotal = capturaSTotal + lpv.precioTotal;

                lProVent.Add(lpv);
                LLenarTablaVenta();

                txt_SubTotal.Text = sumaStotal.ToString();
                txt_Total.Text = sumaStotal.ToString();
            }
            else if (capturarCantidad == 0)
            {
                MessageBox.Show("Debe asignar una cantidad!");
            }

            txt_CantidadProducto.Text = "";
            txt_FiltrarNombre.Text = "";
        }

        public void LLenarTablaVenta()
        {
            IcvProductosVenta = CollectionViewSource.GetDefaultView(lProVent);

            if (IcvProductosVenta != null)
            {
                //Se reinicia el dataGrid borrando los valores que almacena
                dg_ListaProductosVenta.ItemsSource = null;
                dg_ListaProductosVenta.Items.Clear();

                //Se le dan propiedades al dataGrid y se le asignan datos
                dg_ListaProductosVenta.AutoGenerateColumns = true;
                dg_ListaProductosVenta.IsReadOnly = true;
                dg_ListaProductosVenta.MinColumnWidth = 108;
                dg_ListaProductosVenta.MaxColumnWidth = 300;
                dg_ListaProductosVenta.ItemsSource = IcvProductosVenta;
                dg_ListaProductosVenta.Items.Refresh();
            }
        }

        private void btn_EliminarProducto_Click(object sender, RoutedEventArgs e)
        {
            decimal capturaSTotal = 0;
            decimal restaStotal = 0;
            decimal.TryParse(txt_SubTotal.Text, out capturaSTotal);

            ListaProductoVenta lpv = new ListaProductoVenta();
            if (dg_ListaProductos.SelectedIndex >= 0)
            {
                lpv = (ListaProductoVenta)dg_ListaProductosVenta.SelectedValue;
                restaStotal = capturaSTotal - lpv.precioTotal;

                lProVent.Remove(lpv);
                LLenarTablaVenta();

                txt_SubTotal.Text = restaStotal.ToString();
                txt_Total.Text = restaStotal.ToString();
            }
        }

        private void btn_Facturar_Click(object sender, RoutedEventArgs e)
        {
            int resultadoGuardar;
            int nuevaCantidadProducto;

            decimal descuento;
            decimal.TryParse(txt_Descuento.Text, out descuento);

            Venta vt = new Venta();
            AdministrarVenta av = new AdministrarVenta();

            vt.codigo = captura[1];
            vt.identCliente = txt_Identificacion.Text;
            vt.listaProductos = JsonConvert.SerializeObject(lProVent);
            vt.subTotal = decimal.Parse(txt_SubTotal.Text);
            vt.descuento = descuento;
            vt.total = decimal.Parse(txt_Total.Text);
            vt.tipoPago = cb_TipoPago.SelectedIndex.ToString();
            if (cb_TipoPago.SelectedIndex == 0)
            {
                vt.montoAbono = decimal.Parse(txt_Total.Text);
            }
            if (cb_TipoPago.SelectedIndex == 1)
            {
                vt.montoAbono = decimal.Parse(txt_ValorAbono.Text);
            }
            vt.observacion = txt_Observaciones.Text;
            vt.usuarioModifica = int.Parse(Conexion.Utilidades.IdUsuario);
            resultadoGuardar = av.InsertarVenta(vt);
            if (resultadoGuardar == 1)
            {
                foreach (ListaProductoVenta lpg in lProVent)
                {
                    foreach (ListaProductoFactura lpf in lProfact)
                    {
                        if (lpg.codigo.Equals(lpf.codigo))
                        {
                            nuevaCantidadProducto = lpf.cantidad - lpg.cantidad;
                            AdministrarProducto.ActualizarCantidadProducto(lpg.codigo, nuevaCantidadProducto, 0);
                        }
                    }
                }
            }

            if (cb_GenerarPdf.IsChecked.Value)
            {
                Entidades.GenerarPdf.GenerarPdfFactura(per, lProVent, vt, captura[1]);
            }

            LimpiarCampos();
            LLenarTabla();
            ObtenerCodigoFactura();
            MessageBox.Show("Venta exitosa!");

            if (cb_GenerarPdf.IsChecked.Value)
            {
                Entidades.GenerarPdf.GenerarPdfFactura(per, lProVent, vt, captura[1]);
            }
        }



        private void btn_Nuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            //se borra los datos de todos los controles TextBox, CheckBox y ComboBox
            txt_Identificacion.Text = "";
            txt_Nombre.Text = "";
            txt_Telefono.Text = "";
            txt_Direccion.Text = "";
            txt_SubTotal.Text = "";
            txt_Descuento.Text = "";
            txt_Total.Text = "";
            txt_Observaciones.Text = "";
            cb_TipoPago.SelectedIndex = -1;
            cb_GenerarPdf.IsChecked = false;

            //Se reinicia el dataGrid borrando los valores que almacena
            dg_ListaProductosVenta.ItemsSource = null;
            dg_ListaProductosVenta.Items.Clear();
            lProVent.Clear();
            LLenarTablaVenta();

            //Se habilita el campo cedula cliente se deshabilita el boton limpiar cliente
            txt_Identificacion.IsEnabled = true;
            txt_Identificacion.Focus();
            btn_refrescar.IsEnabled = false;
        }
    }
}