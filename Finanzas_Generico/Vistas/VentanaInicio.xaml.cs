﻿using Finanzas_Generico.Conexion;
using Finanzas_Generico.Entidades;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace Finanzas_Generico.Vistas
{
    /// <summary>
    /// Interaction logic for VentanaInicio.xaml
    /// </summary>
    public partial class VentanaInicio : Window
    {
        private string mensaje = string.Empty;

        public VentanaInicio(string mensaje)
        {
            InitializeComponent();
            this.mensaje = mensaje;
            lblMensaje.Content = mensaje;
            this.Title = string.Format("Bienvenido {0}", Conexion.Utilidades.Usuario);
            if (Utilidades.licencia == false)
            {
                this.opcVenta.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.opcVenta.Visibility = Visibility.Visible;
            }

            //btnPrueba.Visibility = Visibility.Visible;
            //prueba2.Visibility = Visibility.Visible;
            btnPrueba.Visibility = Visibility.Hidden;
            prueba2.Visibility = Visibility.Hidden;
        }

        private void miAgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            IngresarProducto ip = new IngresarProducto();
            ip.Owner = this;
            ip.ShowDialog();
        }

        private void CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = !this.DialogResult.HasValue ? false : this.DialogResult;
            this.Close();
        }

        private void miActualizarProducto_Click(object sender, RoutedEventArgs e)
        {
            ActualizarProducto ap = new ActualizarProducto();
            ap.Owner = this;
            ap.ShowDialog();
        }

        private void miActualizarCliente_Click(object sender, RoutedEventArgs e)
        {
            ActualizarCliente ac = new ActualizarCliente();
            ac.Owner = this;
            ac.ShowDialog();
        }

        private void miAgregarCliente_Click(object sender, RoutedEventArgs e)
        {
            IngresarCliente ic = new IngresarCliente();
            ic.Owner = this;
            ic.ShowDialog();
        }

        private void miListaProductos_Click(object sender, RoutedEventArgs e)
        {
            ListaProductos lp = new ListaProductos();
            lp.Owner = this;
            lp.ShowDialog();
        }

        private void miListaCliente_Click(object sender, RoutedEventArgs e)
        {
            ListaClientes lc = new ListaClientes();
            lc.Owner = this;
            lc.ShowDialog();
        }

        private void image_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void miGenerarFactura_Click(object sender, RoutedEventArgs e)
        {
            GenerarFactura gf = new GenerarFactura();
            gf.Owner = this;
            gf.ShowDialog();
        }

        private void btnPrueba_Click(object sender, RoutedEventArgs e)
        {
            ListaProducto lp = new ListaProducto();
            List<ListaProducto> lProducto = new List<ListaProducto>();

            lp.codigo = "001";
            lp.nombre = "p1";
            lp.precio = 2000;
            lp.cantidad = 2;
            lp.cantidadMinima = 1;
            lp.descripcion = "dfsdasd";
            lProducto.Add(lp);

            lp.codigo = "002";
            lp.nombre = "p2";
            lp.precio = 3000;
            lp.cantidad = 20;
            lp.cantidadMinima = 10;
            lp.descripcion = "dfsdaasdasdsd";
            lProducto.Add(lp);

            lp.codigo = "003";
            lp.nombre = "p3";
            lp.precio = 5000;
            lp.cantidad = 12;
            lp.cantidadMinima = 5;
            lp.descripcion = "minmin";
            lProducto.Add(lp);

            string SJson = JsonConvert.SerializeObject(lProducto);

            List<ListaProducto> lProducto2 = new List<ListaProducto>();

            lProducto2 = JsonConvert.DeserializeObject<List<ListaProducto>>(SJson);

            JObject jsonObjet = new JObject(new JProperty("productos", new JArray(from lps in lProducto2
                                                                                  select new JObject(
                                                                                      new JProperty("codigo", lps.codigo),
                                                                                      new JProperty("nombre", lps.nombre),
                                                                                      new JProperty("precio", lps.precio),
                                                                                      new JProperty("cantidad", lps.cantidad),
                                                                                      new JProperty("cantidad_minima", lps.cantidadMinima),
                                                                                      new JProperty("descripcion", lps.descripcion)
                                                                                      ))));

            //JObject jsonObjet1 = (JObject)jsonObjet["productos"];

            //MessageBox.Show(jsonObjet1[0]["codigo"].ToString());
        }

        private void prueba2_Click(object sender, RoutedEventArgs e)
        {
            DateTime fechaActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            string CapturaFecha = fechaActual.ToString("d", DateTimeFormatInfo.InvariantInfo);
            MessageBox.Show(""+ Environment.UserName);
        }

        private void InsertarLicencia_Click(object sender, RoutedEventArgs e)
        {
            InsertarLicencia il = new Vistas.InsertarLicencia();
            il.ShowDialog();
            if (il.DialogResult == true)
            {
                this.DataContext = null;
                this.DataContext = new VentanaInicio(this.mensaje);
            }
        }

        private void miConsultarFactura_Click(object sender, RoutedEventArgs e)
        {
            ConsultarVentas cv = new ConsultarVentas();
            cv.Owner = this;
            cv.ShowDialog();
        }
    }
}