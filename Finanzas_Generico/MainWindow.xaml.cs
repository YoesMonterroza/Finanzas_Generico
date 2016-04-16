using Finanzas_Generico.Conexion;
using Finanzas_Generico.Entidades;
using Finanzas_Generico.Manager;
using Finanzas_Generico.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Finanzas_Generico
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoguin_Click(object sender, RoutedEventArgs e)
        {
            string user = txtUsuario.Text;
            string pass = Utilidades.ConvertirHash(txtPassBox.Password);
            Usuario usuario = new Usuario();
            usuario = AdministrarUsuario.Consultar(user);
            if (!usuario.id.Equals("error"))
            {
                if (usuario.pass.Equals(pass))
                {
                    Utilidades.Usuario = usuario.nick;
                    VentanaInicio vi = new VentanaInicio();
                    vi.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("El usuario y/o la contraseña son incorrectos", "Error");
                    txtPassBox.Clear();
                }
            }
            else
            {
                MessageBox.Show("El usuario y/o la contraseña son incorrectos", "Error");
                txtPassBox.Clear();
            }
        }

        private void lblRecuperaContrasena_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //this.Hide();
            RecuperarContrasena rc = new RecuperarContrasena();
            rc.ShowDialog();
        }
    }
}