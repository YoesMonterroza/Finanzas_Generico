using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using Finanzas_Generico.Conexion;
using Finanzas_Generico.Entidades;

namespace Finanzas_Generico.Manager
{
    public class AdministrarProducto
    {
        private MySqlDataAdapter da;
        public DataTable dTable { get; set; }

        public static int InsertarUsuario(Producto pr)
        {
            int binario = 0;
            using (MySqlCommand cmd = new MySqlCommand())
            {
                try
                {
                    // setear parametros del command
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = ConexionDB.Conexion();
                    cmd.CommandText = "SpProductoInsertar";

                    //asignar paramentros
                    cmd.Parameters.AddWithValue("p_codigo", pr.codigo);
                    cmd.Parameters.AddWithValue("p_nombre", pr.nombre);
                    cmd.Parameters.AddWithValue("p_cantidad", pr.cantidad);
                    cmd.Parameters.AddWithValue("p_cantidadMinima", pr.cantidadMinima);
                    cmd.Parameters.AddWithValue("p_precio", pr.precio);
                    cmd.Parameters.AddWithValue("p_usuarioModifica", pr.usuarioModifica);

                    //abrir la conexion
                    ConexionDB.Conexion().Open();

                    //ejecutar el query
                    cmd.ExecuteNonQuery();

                    binario = 1;
                }
                catch (MySqlException ex)
                {
                    binario = 0;
                    throw ex;
                }
                finally
                {
                    ConexionDB.Conexion().Close();
                } // end try
            } // end using
            return binario;
        } // end GuardarHuella

        public DataTable ConsultarProducto(String sCodigo)
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                try
                {
                    ConexionDB.Conexion().Open();

                    // setear parametros del command
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = ConexionDB.Conexion();
                    cmd.CommandText = "SpProductoConsultar";
                    cmd.Parameters.AddWithValue("p_codigo", sCodigo);

                    //cmd.ExecuteNonQuery();
                    //da = new MySqlDataAdapter(cmd);
                    //dTable = new DataTable();
                    //da.Fill(dTable);
                    MySqlDataReader lectorProductos;
                    lectorProductos = cmd.ExecuteReader();

                    //String nombre;
                    //nombre = Convert.ToString(lectorProductos["nombre"]);
                    if (lectorProductos.HasRows)
                    {
                        while (lectorProductos.Read())
                        {
                            String nombre;
                            nombre = Convert.ToString(lectorProductos["nombre"]);
                        }
                    }

                    
                }
                catch (MySqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    ConexionDB.Conexion().Close();
                }
            }

                return dTable;
        }
    }
}
