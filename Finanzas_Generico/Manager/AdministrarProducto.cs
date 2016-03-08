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
        //private MySqlDataAdapter da;
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
                    cmd.Parameters.AddWithValue("p_estado", pr.estado);
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
        } // end InsertarUsuario

        public Producto ConsultarProducto(String sCodigo)
        {
            Producto pr = new Producto();
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
                            pr.codigo = Convert.ToString(lectorProductos["codigo"]);
                            pr.nombre = Convert.ToString(lectorProductos["nombre"]);
                            pr.cantidad = Convert.ToInt32(lectorProductos["cantidad"]);
                            pr.cantidadMinima = Convert.ToInt32(lectorProductos["cantidadMinima"]);
                            pr.precio = Convert.ToDecimal(lectorProductos["precio"]);
                            pr.estado = Convert.ToString(lectorProductos["estado"]);
                            pr.descripcion = Convert.ToString(lectorProductos["descripcion"]);
                            pr.usuarioModifica = Convert.ToInt32(lectorProductos["usuarioModifica"]);
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

            return pr;
        }

        public static int ActualizarUsuario(Producto pr)
        {
            int binario = 0;
            using (MySqlCommand cmd = new MySqlCommand())
            {
                try
                {
                    // setear parametros del command
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = ConexionDB.Conexion();
                    cmd.CommandText = "SpProductoActualizar";

                    //asignar paramentros
                    cmd.Parameters.AddWithValue("p_codigo", pr.codigo);
                    cmd.Parameters.AddWithValue("p_nombre", pr.nombre);
                    cmd.Parameters.AddWithValue("p_cantidad", pr.cantidad);
                    cmd.Parameters.AddWithValue("p_cantidadMinima", pr.cantidadMinima);
                    cmd.Parameters.AddWithValue("p_precio", pr.precio);
                    cmd.Parameters.AddWithValue("p_estado", pr.estado);
                    cmd.Parameters.AddWithValue("p_descripcion", pr.descripcion);
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

        public static List<ListaProducto> ListaProductos()
        {
            //Producto pr = new Producto();
            ListaProducto lPro;
            List<ListaProducto> listPro = new List<ListaProducto>();
            using (MySqlCommand cmd = new MySqlCommand())
            {
                try
                {
                    ConexionDB.Conexion().Open();

                    // setear parametros del command
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = ConexionDB.Conexion();
                    cmd.CommandText = "SpProductoListar";
                    
                    MySqlDataReader lectorProductos;
                    lectorProductos = cmd.ExecuteReader();
                    
                    if (lectorProductos.HasRows)
                    {
                        while (lectorProductos.Read())
                        {
                            lPro = new ListaProducto();
                            lPro.codigo = Convert.ToString(lectorProductos["Código"]);
                            lPro.nombre = Convert.ToString(lectorProductos["Nombre"]);
                            lPro.cantidad = Convert.ToInt32(lectorProductos["Cantidad"]);
                            lPro.cantidadMinima = Convert.ToInt32(lectorProductos["Cantidad_Mínima"]);
                            lPro.precio = Convert.ToDecimal(lectorProductos["Precio"]);
                            lPro.descripcion = Convert.ToString(lectorProductos["Descripción"]);
                            //pr = new ListaProducto(Convert.ToString(lectorProductos["Código"]), Convert.ToString(lectorProductos["Nombre"]), Convert.ToInt32(lectorProductos["Cantidad"]), Convert.ToInt32(lectorProductos["Cantidad_Mínima"]), Convert.ToDecimal(lectorProductos["Precio"]), Convert.ToString(lectorProductos["Descripción"]));
                            listPro.Add(lPro);
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
            return listPro;
        }
    }
}
