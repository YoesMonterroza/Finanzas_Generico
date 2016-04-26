using Finanzas_Generico.Conexion;
using Finanzas_Generico.Entidades;
using Finanzas_Generico.Manager;
using Finanzas_Generico.Vistas;
using System;
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
            txtUsuario.Focus();
        }

        private void btnLoguin_Click(object sender, RoutedEventArgs e)
        {
            string user = txtUsuario.Text;
            string pass = Utilidades.ConvertirHash(txtPassBox.Password);
            Usuario usuario = new Usuario();
            usuario = AdministrarUsuario.Consultar(user);
            //if (usuario.licencia > DateTime.Now)
            //{
            if (!usuario.resultado.Equals("error"))
            {
                if (usuario.pass.Equals(pass))
                {
                    if (usuario.licencia <= DateTime.Now)
                    {
                        MessageBox.Show("Su licencia ha expirado, por favor comuniquese con el proveedor del servicio\n\r" +
                                    "para obtener una nueva.", "Aviso");
                        Utilidades.licencia = false;
                    }
                    Utilidades.IdUsuario = usuario.id;
                    Utilidades.Usuario = usuario.nick;
                    VentanaInicio vi = new VentanaInicio();
                    this.Hide();
                    vi.ShowDialog();
                    if (vi.DialogResult == true)
                    {
                        this.Close();
                    }
                    else
                    {
                        this.Show();
                        txtUsuario.Clear();
                        txtPassBox.Clear();
                        txtUsuario.Focus();
                    }
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
            //}
            //else
            //{
            //    MessageBox.Show("Su licencia ha expirado, por favor comuniquese con el proveedor del servicio\n\r" +
            //        "para obtener una nueva.", "Aviso");
            //}
        }

        private void lblRecuperaContrasena_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //this.Hide();
            RecuperarContrasena rc = new RecuperarContrasena();
            rc.ShowDialog();
        }
    }
}