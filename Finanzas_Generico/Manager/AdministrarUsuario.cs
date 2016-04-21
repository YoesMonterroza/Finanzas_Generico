using Finanzas_Generico.Conexion;
using Finanzas_Generico.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Finanzas_Generico.Manager
{
    public class AdministrarUsuario
    {
        public static void InsertarUsuario(string identificacion, string nick, string pass, int nivelPermiso = 1)
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

        public static int CambiarContrasena(string user, string pass)
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = ConexionDB.Conexion();
                    cmd.CommandText = "spUsuarioCambioContraseña";
                    cmd.Parameters.AddWithValue("i_user", user);
                    cmd.Parameters.AddWithValue("i_pass", pass);
                    ConexionDB.Conexion().Open();

                    return cmd.ExecuteNonQuery();
                }
                finally
                {
                    ConexionDB.Conexion().Close();
                }
            }
        }

        public static List<Preguntas> ListarPreguntas()
        {
            Preguntas p = new Preguntas();
            List<Preguntas> lista = new List<Preguntas>();
            MySqlDataReader reader = Utilidades.Consulta("spPreguntasListar");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    p.id = Convert.ToInt16(reader["id"]);
                    p.pregunta = Convert.ToString(reader["pregunta"]);
                    lista.Add(p);
                }
            }
            ConexionDB.Conexion().Close();
            return lista;
        }

        public static Usuario Consultar(string user)
        {
            Usuario u = new Usuario();
            MySqlDataReader reader = Utilidades.Consulta("spUsuarioConsulta", new Dictionary<string, string>() { { "user", user } });
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    u.id = Convert.ToString(reader["id"]);
                    u.nick = Convert.ToString(reader["nick"]);
                    u.pass = Convert.ToString(reader["pass"]);
                    u.resultado = "ok";
                }
            }
            else
                u.resultado = "error";

            ConexionDB.Conexion().Close();
            return u;
        }

        public static Dictionary<string, string> ConsultaPregunta(string user)
        {
            MySqlDataReader reader = Utilidades.Consulta("spPreguntaConsulta", new Dictionary<string, string>() { { "i_user", user } });
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dic.Add("pregunta", Convert.ToString(reader["pregunta"]));
                    dic.Add("respuesta", Convert.ToString(reader["respuesta"]));
                    dic.Add("ok", "ok");
                }
            }
            else
            {
                dic.Add("ok", "error");
            }

            ConexionDB.Conexion().Close();
            return dic;
        }
    }
}