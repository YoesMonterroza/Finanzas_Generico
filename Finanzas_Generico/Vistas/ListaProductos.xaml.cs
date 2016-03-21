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
        ICollectionView IcvProductos;

        public ListaProductos()
        {
            InitializeComponent();
            LLenarTabla();
        }
        
        public void LLenarTabla()
        {
            IcvProductos = CollectionViewSource.GetDefaultView(AdministrarProducto.ListaProductos());

            if (IcvProductos != null)
            {
                DgListaProductos.AutoGenerateColumns = true;
                DgListaProductos.IsReadOnly = true;
                DgListaProductos.MaxColumnWidth = 300;
                DgListaProductos.ItemsSource = IcvProductos;
            }
        }

        private void txt_filtro_TextChanged(object sender, TextChangedEventArgs e)
        {
            ICollectionView IcvProductosFiltro;
            List<ListaProducto> listPro = new List<ListaProducto>();
            ListaProducto lp = new ListaProducto();

            if (txt_filtro.Text != "" && txt_filtro.Text.Length > 2)
            {
                var filtro = from pro in AdministrarProducto.ListaProductos()
                             where pro.nombre.Contains(txt_filtro.Text)
                             select new { pro.codigo, pro.nombre, pro.precio, pro.cantidad, pro.cantidadMinima, pro.descripcion };

                foreach (var fil in filtro){
                    lp.codigo = fil.codigo;
                    lp.nombre = fil.nombre;
                    lp.precio = fil.precio;
                    lp.cantidad = fil.cantidad;
                    lp.cantidadMinima = fil.cantidadMinima;
                    lp.descripcion = fil.descripcion;
                    listPro.Add(lp);
                }

                IcvProductosFiltro = CollectionViewSource.GetDefaultView(listPro);

                if (IcvProductosFiltro != null)
                {
                    DgListaProductos.AutoGenerateColumns = true;
                    DgListaProductos.IsReadOnly = true;
                    DgListaProductos.MaxColumnWidth = 300;
                    DgListaProductos.ItemsSource = IcvProductosFiltro;
                    //cvPersonnes.Filter = TextFilter;
                }
            }
            else
            {
                LLenarTabla();
            }
        }

        private void btn_mostrar_Click(object sender, RoutedEventArgs e)
        {
            if (DgListaProductos.SelectedIndex >= 0)
            {
                this.Close();
                ListaProducto lp = new ListaProducto();
                lp = (ListaProducto)DgListaProductos.SelectedValue;
                ActualizarProducto ap = new ActualizarProducto(lp.codigo);
                ap.ShowDialog();
            }
        }

        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
