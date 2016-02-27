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
            ConexionDB cp = new ConexionDB();
            
        }
    }
}
