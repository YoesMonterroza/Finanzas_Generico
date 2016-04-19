using Finanzas_Generico.Entidades;
using Finanzas_Generico.Manager;
using System;
using System.Configuration;
using System.Windows;

namespace Finanzas_Generico.Vistas
{
    /// <summary>
    /// Interaction logic for ActualizarProducto.xaml
    /// </summary>
    public partial class ActualizarProducto : Window
    {
        private bool origenListaProducto = false;

        public ActualizarProducto()
        {
            InitializeComponent();
            this.Title = string.Format(ConfigurationManager.AppSettings["formatoTitulos"].ToString(), Conexion.Utilidades.Usuario, "Actualizar Producto");
        }

        public ActualizarProducto(String cod_Producto)
        {
            InitializeComponent();
            AdministrarProducto ap = new AdministrarProducto();
            Producto pr = new Producto();
            pr = ap.ConsultarProducto(cod_Producto);

            txt_CodProducto.Text = cod_Producto;
            txt_Nombre.Text = Convert.ToString(pr.nombre);
            txt_Cantidad.Text = Convert.ToString(pr.cantidad);
            txt_CantidadMinima.Text = Convert.ToString(pr.cantidadMinima);
            txt_Precio.Text = Convert.ToString(pr.precio);
            cb_Estado.SelectedIndex = Convert.ToInt32(pr.estado);
            txt_descripcion.Text = Convert.ToString(pr.descripcion);
            txt_CodProducto.IsEnabled = false;
            ConsultarProducto.IsEnabled = false;
            txt_Nombre.Focus();

            origenListaProducto = true;
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

            if (origenListaProducto == true)
            {
                origenListaProducto = false;
                ListaProductos lp = new ListaProductos();
                lp.ShowDialog();
            }
        }

        public void LimpiarCampos()
        {
            txt_CodProducto.Text = "";
            txt_Nombre.Text = "";
            txt_Cantidad.Text = "";
            txt_CantidadMinima.Text = "";
            txt_Precio.Text = "";
            cb_Estado.SelectedIndex = -1;
            txt_CodProducto.IsEnabled = true;
            txt_descripcion.Text = "";
            ConsultarProducto.IsEnabled = true;
            txt_CodProducto.Focus();
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
            capturarBinario = AdministrarProducto.ActualizarProducto(p);

            if (capturarBinario == 1)
            {
                LimpiarCampos();
                MessageBox.Show("Producto actualizado.");
            }
            else {
                MessageBox.Show("Error al actualizar");
            }
        }
    }
}