using Finanzas_Generico.Manager;
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
    /// Interaction logic for GenerarFactura.xaml
    /// </summary>
    public partial class GenerarFactura : Window
    {
        String[] captura;

        public GenerarFactura()
        {
            InitializeComponent();
            AdministrarFactura af = new AdministrarFactura();
            captura = af.ObtenerConsecutivo();
            lbl_nrofactura.Content = "Codigo de factura: " + captura[1];
        }
        
    }
}
