﻿using Finanzas_Generico.Conexion;
using Finanzas_Generico.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Finanzas_Generico.Manager
{
    public class AdministrarProducto
    {
        //private MySqlDataAdapter da;
        public DataTable dTable { get; set; }

        public static int InsertarProducto(Producto pr)
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
                    cmd.Parameters.AddWithValue("p_descripcion", pr.descripcion);
                    cmd.Parameters.AddWithValue("p_usuarioModifica", pr.usuarioModifica);

                    //abrir la conexion
                    ConexionDB.Conexion().Open();

                    //ejecutar el query
                    binario = cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
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

        public static int ActualizarProducto(Producto pr)
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
                    binario = cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
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
                            lPro.codigo = Convert.ToString(lectorProductos["Codigo"]);
                            lPro.nombre = Convert.ToString(lectorProductos["Nombre"]);
                            lPro.cantidad = Convert.ToInt32(lectorProductos["Cantidad"]);
                            lPro.cantidadMinima = Convert.ToInt32(lectorProductos["Cantidad_Minima"]);
                            lPro.precio = Convert.ToDecimal(lectorProductos["Precio"]);
                            lPro.descripcion = Convert.ToString(lectorProductos["Descripcion"]);
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

        public static List<ListaProductoFactura> ListaProductosFactura()
        {
            //Producto pr = new Producto();
            ListaProductoFactura lPro;
            List<ListaProductoFactura> listPro = new List<ListaProductoFactura>();
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
                            lPro = new ListaProductoFactura();
                            lPro.codigo = Convert.ToString(lectorProductos["Codigo"]);
                            lPro.nombre = Convert.ToString(lectorProductos["Nombre"]);
                            lPro.cantidad = Convert.ToInt32(lectorProductos["Cantidad"]);
                            lPro.precio = Convert.ToDecimal(lectorProductos["Precio"]);
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

        public static int ActualizarCantidadProducto(String codigo, int cantidad, int usuarioModifica)
        {
            int binario = 0;
            using (MySqlCommand cmd = new MySqlCommand())
            {
                try
                {
                    // setear parametros del command
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = ConexionDB.Conexion();
                    cmd.CommandText = "SpProductoActualizarCantidad";

                    //asignar paramentros
                    cmd.Parameters.AddWithValue("p_codigo", codigo);
                    cmd.Parameters.AddWithValue("p_cantidad", cantidad);
                    cmd.Parameters.AddWithValue("p_usuarioModifica", usuarioModifica);

                    //abrir la conexion
                    ConexionDB.Conexion().Open();

                    //ejecutar el query
                    binario = cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    ConexionDB.Conexion().Close();
                } // end try
            } // end using
            return binario;
        }
    }
}