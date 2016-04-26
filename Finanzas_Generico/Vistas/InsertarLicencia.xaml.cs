using Finanzas_Generico.Conexion;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;

namespace Finanzas_Generico.Vistas
{
    /// <summary>
    /// Interaction logic for InsertarLicencia.xaml
    /// </summary>
    public partial class InsertarLicencia : Window
    {
        private VentanaInicio vi;

        public InsertarLicencia()
        {
            InitializeComponent();
            txtLicencia.Focus();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            string licencia = Utilidades.DesEncriptarString(txtLicencia.Text);
            string[] split = licencia.Split(' ');

            try
            {
                if (split[0] == "Arrecife")
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = ConexionDB.Conexion();
                            cmd.CommandText = "SpCambiarLicencia";

                            cmd.Parameters.AddWithValue("p_fechaLicencia", Convert.ToDateTime(split[1]));
                            cmd.Parameters.AddWithValue("p_identificacion", Utilidades.IdUsuario);

                            //abrir la conexion
                            ConexionDB.Conexion().Open();

                            //ejecutar el query
                            int result = cmd.ExecuteNonQuery();

                            if (result != -1)
                            {
                                string format = string.Format("La licencia se ha actualizado, la nueva fecha de vencimiento es\n\rel {0}", split[1]);
                                MessageBox.Show(format, "Aviso");
                                Utilidades.licencia = true;
                                this.DialogResult = true;
                                this.Close();
                            }
                        }
                        catch
                        {
                            throw;
                        }
                        finally
                        {
                            ConexionDB.Conexion().Close();
                        } // end try
                    } // end using
                }
                else
                {
                    throw new System.FieldAccessException();
                }
            }
            catch
            {
                MessageBox.Show("La licencia que ha insertado no es valida, por favor verifiquela.", "Error");
            }
        }
    }
}