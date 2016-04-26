using Finanzas_Generico.Conexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerarLicencias
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string cadena = Utilidades.EncriptarString(string.Format("Arrecife {0}", DateTime.Now.AddDays(1)));
            Console.WriteLine(cadena);
            Console.ReadLine();
        }
    }
}