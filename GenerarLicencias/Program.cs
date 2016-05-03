using Finanzas_Generico.Conexion;
using System;

namespace GenerarLicencias
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string cadena = Utilidades.EncriptarString(string.Format("Arrecife {0}", DateTime.Now.AddMonths(2)));
            Console.WriteLine(cadena);
            Console.ReadLine();
        }
    }
}