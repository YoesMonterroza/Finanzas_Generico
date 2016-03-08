﻿using System;
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
using Finanzas_Generico.Entidades;
using Finanzas_Generico.Manager;

namespace Finanzas_Generico.Vistas
{
    /// <summary>
    /// Interaction logic for ActualizarProducto.xaml
    /// </summary>
    public partial class ActualizarProducto : Window
    {
        public ActualizarProducto()
        {
            InitializeComponent();
        }

        private void ConsultarProducto_Click(object sender, RoutedEventArgs e)
        {
            AdministrarProducto ap = new AdministrarProducto();
            Producto pr = new Producto();
            pr = ap.ConsultarProducto(txt_CodProducto.Text);

            txt_Nombre.Text = Convert.ToString(pr.nombre); 
            txt_Cantidad.Text = Convert.ToString(pr.cantidad); 
            txt_CantidadMinima.Text = Convert.ToString(pr.cantidadMinima);
            txt_Precio.Text = Convert.ToString(pr.precio);
            cb_Estado.SelectedIndex = Convert.ToInt32(pr.estado);
            txt_descripcion.Text = Convert.ToString(pr.descripcion);
            txt_CodProducto.IsEnabled = false;
            ConsultarProducto.IsEnabled = false;
        }

        private void btn_Salir_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            this.Close();
        }

        public void LimpiarCampos()
        {
            txt_CodProducto.Text = "";
            txt_Nombre.Text =  "";
            txt_Cantidad.Text = ""; 
            txt_CantidadMinima.Text = "";
            txt_Precio.Text = "";
            cb_Estado.SelectedIndex = -1;
            txt_CodProducto.IsEnabled = true;
            txt_descripcion.Text = "";
            ConsultarProducto.IsEnabled = true;
        }

        private void btn_Nuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void btn_Actualizar_Click(object sender, RoutedEventArgs e)
        {
            int capturarBinario;
            Producto p = new Producto();
            p.codigo = txt_CodProducto.Text;
            p.nombre = txt_Nombre.Text;
            p.cantidad = Int32.Parse(txt_Cantidad.Text);
            p.cantidadMinima = Int32.Parse(txt_CantidadMinima.Text);
            p.precio = Decimal.Parse(txt_Precio.Text);
            p.estado = Convert.ToString(cb_Estado.SelectedIndex);
            p.descripcion = txt_descripcion.Text;
            p.usuarioModifica = 0;
            capturarBinario = AdministrarProducto.ActualizarUsuario(p);
            

            if (capturarBinario == 1){
                LimpiarCampos();
                MessageBox.Show("Producto actualizado.");
            }
            else { 
                MessageBox.Show("Error al actualizar");
            }
        }
        
    }
}