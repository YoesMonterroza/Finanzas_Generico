using Finanzas_Generico.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ListaClientes.xaml
    /// </summary>
    public partial class ListaClientes : Window
    {
        ICollectionView IcvClientes;

        public ListaClientes()
        {
            InitializeComponent();
            IcvClientes = CollectionViewSource.GetDefaultView(AdministrarPersona.ListaPersonas());

            if (IcvClientes != null)
            {
                DgListaClientes.AutoGenerateColumns = true;
                DgListaClientes.IsReadOnly = true;
                DgListaClientes.MaxColumnWidth = 300;
                DgListaClientes.ItemsSource = IcvClientes;
            }
        }
    }
}
