using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using Finanzas_Generico.Conexion;

namespace Finanzas_Generico.Manager
{
    public class AdministrarUsuario
    {
        public static void InsertarUsuario(string identificacion, string nick, string pass, string nivelPermiso)
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                try
                {
                    // setear parametros del command
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = ConexionDB.Conexion();
                    cmd.CommandText = "NOMBRE_DEL_STORED_PROCEDURE";

                    //asignar paramentros
                    cmd.Parameters.AddWithValue("param1", identificacion);
                    cmd.Parameters.AddWithValue("param2", nick);
                    cmd.Parameters.AddWithValue("param3", pass); 
                    cmd.Parameters.AddWithValue("param4", nivelPermiso);

                    //abrir la conexion
                    ConexionDB.Conexion().Open();

                    //ejecutar el query
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    ConexionDB.Conexion().Close();
                } // end try
            } // end using
        } // end GuardarHuella
    }
}
