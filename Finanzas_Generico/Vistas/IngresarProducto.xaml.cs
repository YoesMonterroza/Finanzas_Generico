using Finanzas_Generico.Entidades;
using Finanzas_Generico.Manager;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace Finanzas_Generico.Vistas
{
    /// <summary>
    /// Interaction logic for IngresarProducto.xaml
    /// </summary>
    public partial class IngresarProducto : Window
    {
        public IngresarProducto()
        {
            InitializeComponent();
            this.Title = string.Format(ConfigurationManager.AppSettings["formatoTitulos"].ToString(), Conexion.Utilidades.Usuario, "Ingresar Producto");
            txt_Codigo.Focus();
            cb_Estado.SelectedIndex = 0;
        }

        private void GuardarProducto_Click(object sender, RoutedEventArgs e)
        {
            int capturarBinario;
            Producto p = new Producto();
            p.codigo = txt_Codigo.Text;
            p.nombre = txt_Nombre.Text;
            p.cantidad = Int32.Parse(txt_Cantidad.Text);
            p.cantidadMinima = Int32.Parse(txt_CantidadMinima.Text);
            p.precio = Decimal.Parse(txt_Precio.Text);
            p.estado = Convert.ToString(cb_Estado.SelectedIndex);
            p.descripcion = txt_descripcion.Text;
            p.usuarioModifica = 0;
            capturarBinario = AdministrarProducto.InsertarProducto(p);
            LimpiarCampos();

            if (capturarBinario == 1)
                MessageBox.Show("Producto guardado.");
            else
                MessageBox.Show("Error al guardar");
        }

        public void LimpiarCampos()
        {
            txt_Codigo.Text = "";
            txt_Nombre.Text = "";
            txt_Cantidad.Text = "";
            txt_CantidadMinima.Text = "";
            txt_Precio.Text = "";
            txt_descripcion.Text = "";
            cb_Estado.SelectedIndex = -1;
            cb_Estado.SelectedIndex = 0;
            txt_Codigo.Focus();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}