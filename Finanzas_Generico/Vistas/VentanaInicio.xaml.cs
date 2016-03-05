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

namespace Finanzas_Generico.Vistas
{
    /// <summary>
    /// Interaction logic for VentanaInicio.xaml
    /// </summary>
    public partial class VentanaInicio : Window
    {
        public VentanaInicio()
        {
            InitializeComponent();
        }

        private void conectar_Click(object sender, RoutedEventArgs e)
        {
            //ConexionDB cp = new ConexionDB();
            
        }

        private void miAgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            IngresarProducto ip = new IngresarProducto();
            ip.Owner = this;
            ip.ShowDialog();
        }

        private void CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
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
    }
}
