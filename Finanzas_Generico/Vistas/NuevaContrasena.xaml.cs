using Finanzas_Generico.Conexion;
using Finanzas_Generico.Manager;
using System.Windows;

namespace Finanzas_Generico.Vistas
{
    /// <summary>
    /// Interaction logic for NuevaContrasena.xaml
    /// </summary>
    public partial class NuevaContrasena : Window
    {
        private string user = "";

        public NuevaContrasena(string usuario)
        {
            InitializeComponent();
            this.user = usuario;
        }

        public NuevaContrasena()
        {
            InitializeComponent();
        }

        private void cmdAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (txtContrasena.Password.Equals(txtConfirmaContrasena.Password))
            {
                string newPass = Utilidades.ConvertirHash(txtConfirmaContrasena.Password.ToString());
                int cambio = AdministrarUsuario.CambiarContrasena(this.user, newPass);
                if (cambio != -1)
                {
                    MessageBox.Show("Ya se ha establecido su nueva contraseña.", "Información");
                    this.Close();
                    RecuperarContrasena rc = new RecuperarContrasena();
                    rc.Close();
                }
            }
            else
            {
                MessageBox.Show("Los campos llenados no coinciden, por favor verifiquelos.", "Error");
            }
        }
    }
}