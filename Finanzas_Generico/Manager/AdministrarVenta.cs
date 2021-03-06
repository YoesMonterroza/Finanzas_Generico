﻿using Finanzas_Generico.Conexion;
using Finanzas_Generico.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Finanzas_Generico.Manager
{
    internal class AdministrarVenta
    {
        public String[] ObtenerConsecutivo()
        {
            String consecutivo = "";
            int tamanoCadena = 0;
            String consecutivoGenerado = "";

            String[] arrayString = new String[2];

            using (MySqlCommand cmd = new MySqlCommand())
            {
                try
                {
                    ConexionDB.Conexion().Open();

                    // setear parametros del command
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = ConexionDB.Conexion();
                    cmd.CommandText = "SpConsecutivofactura";

                    MySqlDataReader lectorConsecutivo;
                    lectorConsecutivo = cmd.ExecuteReader();

                    if (lectorConsecutivo.HasRows)
                    {
                        while (lectorConsecutivo.Read())
                        {
                            consecutivo = Convert.ToString(lectorConsecutivo["Consecutivo"]);
                            tamanoCadena = consecutivo.Length;
                        }
                    }

                    switch (tamanoCadena)
                    {
                        case 1:
                            consecutivoGenerado = "000000" + consecutivo;
                            break;

                        case 2:
                            consecutivoGenerado = "00000" + consecutivo;
                            break;

                        case 3:
                            consecutivoGenerado = "0000" + consecutivo;
                            break;

                        case 4:
                            consecutivoGenerado = "000" + consecutivo;
                            break;

                        case 5:
                            consecutivoGenerado = "00" + consecutivo;
                            break;

                        case 6:
                            consecutivoGenerado = "0" + consecutivo;
                            break;

                        case 7:
                            consecutivoGenerado = "" + consecutivo;
                            break;

                        default:
                            break;
                    }
                    arrayString[0] = consecutivo;
                    arrayString[1] = consecutivoGenerado;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    ConexionDB.Conexion().Close();
                }
            }

            return arrayString;
        }

        public int InsertarVenta(Venta vt)
        {
            int binario = 0;
            using (MySqlCommand cmd = new MySqlCommand())
            {
                try
                {
                    // setear parametros del command
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = ConexionDB.Conexion();
                    cmd.CommandText = "SpVentaInsertar";

                    //asignar paramentros
                    cmd.Parameters.AddWithValue("p_consecutivo", vt.codigo);
                    cmd.Parameters.AddWithValue("p_identificacion_cliente", vt.identCliente);
                    cmd.Parameters.AddWithValue("p_json_productos", vt.listaProductos);
                    cmd.Parameters.AddWithValue("p_sub_Total", vt.subTotal);
                    cmd.Parameters.AddWithValue("p_descuento", vt.descuento);
                    cmd.Parameters.AddWithValue("p_total", vt.total);
                    cmd.Parameters.AddWithValue("P_tipoPago", vt.tipoPago);
                    cmd.Parameters.AddWithValue("p_montoAbono", vt.montoAbono);
                    cmd.Parameters.AddWithValue("p_observacion", vt.observacion);
                    cmd.Parameters.AddWithValue("p_UsuarioModifica", vt.usuarioModifica);

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