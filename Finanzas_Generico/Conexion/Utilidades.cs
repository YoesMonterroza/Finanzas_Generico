using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;

//editado 2
namespace Finanzas_Generico.Conexion
{
    public class Utilidades
    {
        public static string IdUsuario { get; set; }
        public static string Usuario { get; set; }
        public static bool? licencia { get; set; }

        /// <summary>
        /// Metodo que convierte una cadena en un SHA1 y luego a un string de base64
        /// </summary>
        /// <param name="cad">cadena de entrada que sera convertida por el metodo</param>
        public static string ConvertirHash(string cad)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bitesEntrada = (new UnicodeEncoding()).GetBytes(cad);
            byte[] hash = sha1.ComputeHash(bitesEntrada);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Metodo para hacer consultas a la base de datos de una manera generica
        /// </summary>
        /// <param name="nameProcedure">string con el nombre del procedimiento almacenado a ejecutar</param>
        /// <param name="parameters">diccionario de strings en el que se pueden colocar los parametros de
        ///         entrada para el procedimiento a ejecutar</param>
        /// <returns>un MySqlDataReader</returns>
        public static MySqlDataReader Consulta(string nameProcedure, Dictionary<string, string> parameters = null)
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConexionDB.Conexion();
                cmd.CommandText = nameProcedure;
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }
                }

                ConexionDB.Conexion().Open();
                MySqlDataReader l = cmd.ExecuteReader();
                return l;
            }
        }

        /// <summary>
        /// metodo para hacer una encriptacion de una cadena, se usa para crear las licencias
        /// </summary>
        /// <param name="cad">string que sera encriptado</param>
        /// <returns>cadena encriptada</returns>
        public static string EncriptarString(string cad)
        {
            try
            {
                string key = "Arrecife"; //llave para encriptar datos
                byte[] keyArray;
                byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(cad);

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();

                //Algoritmo TripleDES
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);
                tdes.Clear();

                //se regresa el resultado en forma de una cadena
                cad = Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error en la encriptacion de los datos\n{0}", e.Data);
            }
            return cad;
        }

        /// <summary>
        /// metodo para deshacer la encriptacion hecha por el metodo EncriptarString
        /// </summary>
        /// <param name="cad">string encriptado</param>
        /// <returns>cadena desencriptada</returns>
        public static string DesEncriptarString(string cad)
        {
            try
            {
                string key = "Arrecife";
                byte[] keyArray;
                byte[] Array_a_Descifrar = Convert.FromBase64String(cad);

                //algoritmo MD5
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);

                tdes.Clear();
                cad = UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error en la desencriptacion de los datos\n{0}", e.Data);
            }
            return cad;
        }
    }
}