using Finanzas_Generico.Entidades;
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
    /// Interaction logic for ActualizarCliente.xaml
    /// </summary>
    public partial class ActualizarCliente : Window
    {
        public ActualizarCliente()
        {
            InitializeComponent();
            txt_Identificacion.Focus();
        }

        private void btn_Salir_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            this.Close();
        }

        private void btn_Buscar_Click(object sender, RoutedEventArgs e)
        {
            AdministrarPersona ap = new AdministrarPersona();
            Persona per = new Persona();
            per = ap.ConsultarPersona(txt_Identificacion.Text);
            
            txt_Nombres.Text = per.nombres;
            txt_Apellidos.Text = per.apellidos;
            txt_Direccion.Text = per.direccion;
            txt_Correo.Text = per.correo;
            txt_Telefono.Text = per.telefono;
            cb_Estado.SelectedIndex = Convert.ToInt32(per.estado);
            txt_Observaciones.Text = per.observaciones;

            btn_Buscar.IsEnabled = false;
            txt_Identificacion.IsEnabled = false;
        }

        private void btn_Actualizar_Click(object sender, RoutedEventArgs e)
        {
            int capturarBinario;
            Persona p = new Persona();
            p.identificacion = txt_Identificacion.Text;
            p.nombres = txt_Nombres.Text;
            p.apellidos = txt_Apellidos.Text;
            p.direccion = txt_Direccion.Text;
            p.correo = txt_Correo.Text;
            p.telefono = txt_Telefono.Text;
            p.estado = Convert.ToString(cb_Estado.SelectedIndex);
            p.observaciones = txt_Observaciones.Text;
            p.usuarioModifica = 0;
            capturarBinario = AdministrarPersona.ActualizarPersona(p);
            

            if (capturarBinario == 1) { 
                LimpiarCampos();
                MessageBox.Show("Perona actualizada.");
            }
            else { 
                MessageBox.Show("Error al actualizar");
            }
        }

        private void btn_Nuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }


        public void LimpiarCampos()
        {
            txt_Identificacion.Text = "";
            txt_Nombres.Text = "";
            txt_Apellidos.Text = "";
            txt_Direccion.Text = "";
            txt_Correo.Text = "";
            txt_Telefono.Text = "";
            cb_Estado.SelectedIndex = 0;
            txt_Observaciones.Text = "";
            txt_Identificacion.Focus();

            btn_Buscar.IsEnabled = true;
            txt_Identificacion.IsEnabled = true;
        }
    }
}
