using Finanzas_Generico.Entidades;
using Finanzas_Generico.Manager;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Finanzas_Generico.Vistas
{
    /// <summary>
    /// Interaction logic for ListaClientes.xaml
    /// </summary>
    public partial class ListaClientes : Window
    {
        private ICollectionView IcvClientes;

        public ListaClientes()
        {
            InitializeComponent();
            this.Title = string.Format(ConfigurationManager.AppSettings["formatoTitulos"].ToString(), Conexion.Utilidades.Usuario, "Lista de Clientes");
            LLenarTabla();
        }

        public void LLenarTabla()
        {
            IcvClientes = CollectionViewSource.GetDefaultView(AdministrarPersona.ListaPersonas());

            if (IcvClientes != null)
            {
                DgListaClientes.AutoGenerateColumns = true;
                DgListaClientes.IsReadOnly = true;
                DgListaClientes.MaxColumnWidth = 300;
                DgListaClientes.ItemsSource = IcvClientes;
            }
        }

        private void txt_filtro_TextChanged(object sender, TextChangedEventArgs e)
        {
            ICollectionView IcvClientesFiltro;
            List<ListaPersona> listper = new List<ListaPersona>();
            ListaPersona lp = new ListaPersona();

            if (txt_filtro.Text != "" && txt_filtro.Text.Length > 2)
            {
                var filtro = from cli in AdministrarPersona.ListaPersonas()
                             where cli.Nombre.Contains(txt_filtro.Text)
                             select new { cli.Identificación, cli.Nombre, cli.Telefono, cli.Correo, cli.Dirección, cli.Observaciones };

                foreach (var fil in filtro)
                {
                    lp.Identificación = fil.Identificación;
                    lp.Nombre = fil.Nombre;
                    lp.Telefono = fil.Telefono;
                    lp.Correo = fil.Correo;
                    lp.Dirección = fil.Dirección;
                    lp.Observaciones = fil.Observaciones;
                    listper.Add(lp);
                }

                IcvClientesFiltro = CollectionViewSource.GetDefaultView(listper);

                if (IcvClientesFiltro != null)
                {
                    DgListaClientes.AutoGenerateColumns = true;
                    DgListaClientes.IsReadOnly = true;
                    DgListaClientes.MaxColumnWidth = 300;
                    DgListaClientes.ItemsSource = IcvClientesFiltro;
                    //cvPersonnes.Filter = TextFilter;
                }
            }
            else
            {
                LLenarTabla();
            }
        }

        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Actulizar_Click(object sender, RoutedEventArgs e)
        {
            if (DgListaClientes.SelectedIndex >= 0)
            {
                ListaPersona lp = new ListaPersona();
                lp = (ListaPersona)DgListaClientes.SelectedValue;
                ActualizarCliente ac = new ActualizarCliente(lp.Identificación);
                ac.ShowDialog();
            }
        }
    }
}