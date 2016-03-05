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

namespace Finanzas_Generico.Vistas
{
    /// <summary>
    /// Interaction logic for IngresarCliente.xaml
    /// </summary>
    public partial class IngresarCliente : Window
    {
        public IngresarCliente()
        {
            InitializeComponent();
        }

        private void btn_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void LimpiarCampos()
        {
            txt_Identificacion.Text = "";
            txt_Nombres.Text = "";
            txt_Apellidos.Text = "";
            txt_Direccion.Text = "";
            txt_Correo.Text = "";
            txt_Telefono.Text = "";
            cb_Estado.SelectedIndex = -1;
            txt_Identificacion.Focus();

        }

        private void btn_Nuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }
    }
}
