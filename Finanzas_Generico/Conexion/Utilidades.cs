﻿using MySql.Data.MySqlClient;
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
        public static string Usuario { get; set; }

        public static string ConvertirHash(string cad)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bitesEntrada = (new UnicodeEncoding()).GetBytes(cad);
            byte[] hash = sha1.ComputeHash(bitesEntrada);
            return Convert.ToBase64String(hash);
        }

        public static MySqlDataReader Consulta(string nameProcedure, Dictionary<string, string> parameters = null)
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConexionDB.Conexion();
                cmd.CommandText = nameProcedure;
                if (parameters != null)
                {
                    foreach (var i in parameters)
                    {
                        cmd.Parameters.AddWithValue(i.Key, i.Value);
                    }
                }

                ConexionDB.Conexion().Open();
                MySqlDataReader l = cmd.ExecuteReader();
                return l;
            }
        }
    }
}