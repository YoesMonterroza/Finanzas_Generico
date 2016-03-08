using Finanzas_Generico.Entidades;
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
    /// Interaction logic for ListaProductos.xaml
    /// </summary>
    public partial class ListaProductos : Window
    {
        private List<Producto> pro;
        ICollectionView IcvProductos;

        public ListaProductos()
        {
            InitializeComponent();

            //pro = new List<Producto>();
            
            //pro = AdministrarProducto.ListaProductos();

            IcvProductos = CollectionViewSource.GetDefaultView(AdministrarProducto.ListaProductos());

            if (IcvProductos != null)
            {
                DgListaProductos.AutoGenerateColumns = true;
                DgListaProductos.ItemsSource = IcvProductos;
                //cvPersonnes.Filter = TextFilter;
            }
        }
    }
}
