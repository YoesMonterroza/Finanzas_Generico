using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Finanzas_Generico.Conexion
{
    public class ConexionDB
    {
        private static MySqlConnection connMY = new MySqlConnection();
        MySqlConnection Conn = new MySqlConnection();
        string connString;

        public ConexionDB()
        {
            InicializarConexion();
        }

        private void InicializarConexion()
        {
            try
            {
                connString = "Server=127.0.0.1; Database=finanzasdb; Uid=root; Pwd=Diaz-1993";
                Conn.ConnectionString = connString;
                Conn.Open();
                MessageBox.Show("Conexion exitosa");
                Conn.Close();
            }
            catch (MySqlException me)
            {
                MessageBox.Show("Error en inicializar conexion" + me);
            }

        }

        private static string CrearCadena()
        {
            //String para cadena de conexion
            StringBuilder sCadena = new StringBuilder("");

            sCadena.Append("Server=<SERVIDOR>;");
            sCadena.Append("Port=<PUERTO>;");
            sCadena.Append("DataBase=<BASE>;");
            sCadena.Append("Uid=<USER>;");
            sCadena.Append("Pwd=<PASSWORD>;");
            sCadena.Replace("<SERVIDOR>", "127.0.0.1");
            sCadena.Replace("<PUERTO>", "3306");
            sCadena.Replace("<BASE>", "finanzasdb");
            sCadena.Replace("<USER>", "root");
            sCadena.Replace("<PASSWORD>", "Diaz-1993");

            return Convert.ToString(sCadena);
        } // end CrearConexion()


        public static MySqlConnection Conexion()
        {
            // objeto de conexion
            if (connMY.State != System.Data.ConnectionState.Open)
                connMY.ConnectionString = CrearCadena();
            return connMY;
        } // end Conexion()
    }
}
