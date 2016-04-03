using Finanzas_Generico.Entidades;
using Finanzas_Generico.Manager;
using MySql.Data.MySqlClient;
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
    /// Interaction logic for RecuperarContrasena.xaml
    /// </summary>
    public partial class RecuperarContrasena : Window
    {
        public RecuperarContrasena()
        {
            InitializeComponent();
            List<Preguntas> lista = AdministrarUsuario.ListarPreguntas();
            foreach (var i in lista)
                cmbPregunta.Items.Add(i.pregunta);
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUser.Text;
            string pregunta = cmbPregunta.SelectedItem.ToString();
            string respuesta = txtRespuesta.Text;
            Dictionary<string, string> reader = AdministrarUsuario.ConsultaPregunta(usuario);
            if (!reader["ok"].Equals("error"))
            {
                if (pregunta.Equals(reader["pregunta"]) && respuesta.Equals(reader["respuesta"]))
                {
                    NuevaContrasena nc = new NuevaContrasena(usuario);
                    nc.ShowDialog();
                }
                else
                {
                    MessageBox.Show("La pregunta y/o respuesta no coincide con la registrada.", "Error");
                }
            }
            else
            {
                MessageBox.Show("El usuario escrito no existe", "Error");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}