using Finanzas_Generico.Conexion;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzas_Generico.Manager
{
    class AdministrarFactura
    {
        public String[] ObtenerConsecutivo()
        {
            String consecutivo = "";
            int tamanoCadena = 0;
            String consecutivoGenerado = "";

            String[] arrayString = new String[2];

            using (MySqlCommand cmd = new MySqlCommand()) {

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
    }
}
