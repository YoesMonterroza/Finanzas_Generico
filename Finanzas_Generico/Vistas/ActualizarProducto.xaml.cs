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
            ap.ConsultarProducto(txt_CodProducto.Text);
        }
    }
}
