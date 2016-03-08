using Finanzas_Generico.Conexion;
using Finanzas_Generico.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzas_Generico.Manager
{
    public class AdministrarPersona
    {
        //private MySqlDataAdapter da;
        public DataTable dTable { get; set; }

        public static int InsertarPersona(Persona pr)
        {
            int binario = 0;
            using (MySqlCommand cmd = new MySqlCommand())
            {
                try
                {
                    // setear parametros del command
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = ConexionDB.Conexion();
                    cmd.CommandText = "SpPersonaInsertar";

                    //asignar paramentros
                    cmd.Parameters.AddWithValue("p_identificacion", pr.identificacion);
                    cmd.Parameters.AddWithValue("p_nombres", pr.nombres);
                    cmd.Parameters.AddWithValue("p_apellidos", pr.apellidos);
                    cmd.Parameters.AddWithValue("p_telefono", pr.telefono);
                    cmd.Parameters.AddWithValue("p_correo", pr.correo);
                    cmd.Parameters.AddWithValue("p_direccion", pr.direccion);
                    cmd.Parameters.AddWithValue("p_observaciones", pr.observaciones);
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
        
        public Persona ConsultarPersona(String sIdentificacion)
        {
            Persona per = new Persona();
            using (MySqlCommand cmd = new MySqlCommand())
            {
                try
                {
                    ConexionDB.Conexion().Open();

                    // setear parametros del command
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = ConexionDB.Conexion();
                    cmd.CommandText = "SpPersonaConsultar";
                    cmd.Parameters.AddWithValue("p_identificacion", sIdentificacion);
                    
                    MySqlDataReader lectorProductos;
                    lectorProductos = cmd.ExecuteReader();

                    if (lectorProductos.HasRows)
                    {
                        while (lectorProductos.Read())
                        {
                            per.nombres = Convert.ToString(lectorProductos["nombres"]);
                            per.apellidos = Convert.ToString(lectorProductos["apellidos"]);
                            per.telefono = Convert.ToString(lectorProductos["telefono"]);
                            per.correo = Convert.ToString(lectorProductos["correo"]);
                            per.direccion = Convert.ToString(lectorProductos["direccion"]);
                            per.observaciones = Convert.ToString(lectorProductos["observaciones"]);
                            per.estado = Convert.ToString(lectorProductos["estado"]);
                            per.usuarioModifica = Convert.ToInt32(lectorProductos["usuarioModifica"]);
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

            return per;
        }

        public static int ActualizarPersona(Persona pr)
        {
            int binario = 0;
            using (MySqlCommand cmd = new MySqlCommand())
            {
                try
                {
                    // setear parametros del command
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = ConexionDB.Conexion();
                    cmd.CommandText = "SpPersonaActualizar";

                    //asignar paramentros
                    cmd.Parameters.AddWithValue("p_identificacion", pr.identificacion);
                    cmd.Parameters.AddWithValue("p_nombres", pr.nombres);
                    cmd.Parameters.AddWithValue("p_apellidos", pr.apellidos);
                    cmd.Parameters.AddWithValue("p_telefono", pr.telefono);
                    cmd.Parameters.AddWithValue("p_correo", pr.correo);
                    cmd.Parameters.AddWithValue("p_direccion", pr.direccion);
                    cmd.Parameters.AddWithValue("p_observaciones", pr.observaciones);
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
        } // fin ActualizarUsuario

        public static List<ListaPersona> ListaPersonas()
        {
            //Producto pr = new Producto();
            ListaPersona lPer;
            List<ListaPersona> listPer = new List<ListaPersona>();
            using (MySqlCommand cmd = new MySqlCommand())
            {
                try
                {
                    ConexionDB.Conexion().Open();

                    // setear parametros del command
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = ConexionDB.Conexion();
                    cmd.CommandText = "SpPersonaListar";

                    MySqlDataReader lectorProductos;
                    lectorProductos = cmd.ExecuteReader();

                    if (lectorProductos.HasRows)
                    {
                        while (lectorProductos.Read())
                        {
                            lPer = new ListaPersona();
                            lPer.Identificación = Convert.ToString(lectorProductos["identificacion"]);
                            lPer.Nombre = Convert.ToString(lectorProductos["nombres"]) + " " + Convert.ToString(lectorProductos["apellidos"]);
                            lPer.Telefono = Convert.ToString(lectorProductos["telefono"]);
                            lPer.Correo = Convert.ToString(lectorProductos["correo"]);
                            lPer.Dirección = Convert.ToString(lectorProductos["direccion"]);
                            lPer.Observaciones = Convert.ToString(lectorProductos["observaciones"]);
                            listPer.Add(lPer);
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
            return listPer;
        }
    }
}
