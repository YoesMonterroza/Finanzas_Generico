using Finanzas_Generico.Conexion;
using Finanzas_Generico.Entidades;
using Finanzas_Generico.Manager;
using Finanzas_Generico.Vistas;
using System.Windows;
using System.Windows.Input;

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