using System;

namespace Finanzas_Generico.Entidades
{
    public struct Usuario
    {
        public String id { get; set; }
        public String identificacion { get; set; }
        public String nick { get; set; }
        public String pass { get; set; }
        public String nivelPermiso { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaModificacion { get; set; }
        public DateTime usuarioModifica { get; set; }
        public int pregunta { get; set; }
        public string Respuesta { get; set; }
    }

    public struct Preguntas
    {
        public int id { get; set; }
        public string pregunta { get; set; }
    }
}