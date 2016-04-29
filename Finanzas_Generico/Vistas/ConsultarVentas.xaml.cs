using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Finanzas_Generico.Conexion;
using MySql.Data.MySqlClient;
using Finanzas_Generico.Entidades;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Finanzas_Generico.Vistas
{
    /// <summary>
    /// Interaction logic for ConsultarVentas.xaml
    /// </summary>
    public partial class ConsultarVentas : Window
    {
        private ICollectionView IcvTablaventas;
        Venta vt;
        List<Venta> lstVenta = new List<Venta>();
        List<TablaListaVenta> tLstVenta = new List<TablaListaVenta>();
        MySqlDataReader reader2;
        private Persona per = new Persona();

        public ConsultarVentas()
        {
            InitializeComponent();
        }

        private void btn_Buscar_Click(object sender, RoutedEventArgs e)
        {
            lstVenta.Clear();
            tLstVenta.Clear();
            TablaListaVenta tlb = new TablaListaVenta();
            string sIdentificacion = txt_Identificacion.Text;
            MySqlDataReader reader1 = null;
            reader2 = null;


            if (sIdentificacion != "")
            {
                reader1 = Utilidades.Consulta("SpConsultarVenta", new Dictionary<string, string>() { { "p_identificacion", sIdentificacion } });
                if (reader1 != null)
                {
                    while (reader1.Read())
                    {
                        vt = new Venta();
                        vt.codigo = Convert.ToString(reader1["consecutivo"]);
                        vt.identCliente = Convert.ToString(reader1["identificacion_cliente"]);
                        vt.listaProductos = Convert.ToString(reader1["json_productos"]);
                        vt.subTotal = Convert.ToDecimal(reader1["sub_Total"]);
                        vt.descuento = Convert.ToDecimal(reader1["descuento"]);
                        vt.total = Convert.ToDecimal(reader1["total"]);
                        vt.tipoPago = Convert.ToString(reader1["tipoPago"]);
                        vt.montoAbono = Convert.ToDecimal(reader1["montoAbono"]);
                        vt.observacion = Convert.ToString(reader1["observacion"]);
                        vt.fechaCreacion = Convert.ToDateTime(reader1["fechaIngreso"]);
                        vt.usuarioModifica = Convert.ToInt32(reader1["usuarioModifica"]);
                        lstVenta.Add(vt);
                    }
                    ConexionDB.Conexion().Close();

                    reader2 = Utilidades.Consulta("SpPersonaConsultar", new Dictionary<string, string>() { { "p_identificacion", sIdentificacion } });

                    while (reader2.Read())
                    {
                        per.identificacion = Convert.ToString(reader2["identificacion"]);
                        per.nombres = Convert.ToString(reader2["nombres"]);
                        per.apellidos = Convert.ToString(reader2["apellidos"]);
                        per.telefono = Convert.ToString(reader2["telefono"]);
                        per.correo = Convert.ToString(reader2["correo"]);
                        per.direccion = Convert.ToString(reader2["direccion"]);
                        per.observaciones = Convert.ToString(reader2["observaciones"]);
                    }
                    ConexionDB.Conexion().Close();

                    txt_nombres.Text = per.nombres + " " + per.apellidos;
                    txt_telefono.Text = per.telefono;
                    txt_direccion.Text = per.direccion;

                    var vListVenta = from ven in lstVenta
                                     select new { ven.codigo, ven.subTotal, ven.descuento, ven.total, ven.fechaCreacion };

                    foreach (var itemlist in vListVenta)
                    {
                        tlb.codigo = itemlist.codigo;
                        tlb.subTotal = itemlist.subTotal;
                        tlb.descuento = itemlist.descuento;
                        tlb.total = itemlist.total;
                        tlb.fechaCreacion = itemlist.fechaCreacion;
                        tLstVenta.Add(tlb);
                    }

                    IcvTablaventas = CollectionViewSource.GetDefaultView(tLstVenta);

                    dg_ListVentCliente.AutoGenerateColumns = true;
                    dg_ListVentCliente.IsReadOnly = true;
                    dg_ListVentCliente.MinColumnWidth = 94;
                    dg_ListVentCliente.MaxColumnWidth = 300;
                    dg_ListVentCliente.ItemsSource = IcvTablaventas;
                }
                else {
                    MessageBox.Show("El cliente no ha realizado compras...!");
                }
            }

        }

        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_GenerarPDF_Click(object sender, RoutedEventArgs e)
        {
            List<TablaListaVenta> tLstVenta = new List<TablaListaVenta>();
            if (dg_ListVentCliente.SelectedIndex >= 0)
            {
                TablaListaVenta tlv = new TablaListaVenta();
                tlv = (TablaListaVenta)dg_ListVentCliente.SelectedValue;
                Venta impVenta = new Venta();

                foreach (var itemVenta in lstVenta)
                {
                    if (tlv.codigo == itemVenta.codigo)
                    {
                        impVenta = itemVenta;
                    }
                }

                List<ListaProductoVenta> lProducto = new List<ListaProductoVenta>();
                lProducto = JsonConvert.DeserializeObject<List<ListaProductoVenta>>(impVenta.listaProductos);
                Entidades.GenerarPdf.GenerarPdfFactura(per, lProducto, impVenta, impVenta.codigo);
            }

        }
    }
}
