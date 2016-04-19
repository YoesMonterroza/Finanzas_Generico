using System;

namespace Finanzas_Generico.Entidades
{
    public struct Persona
    {
        public String id { get; set; }
        public String identificacion { get; set; }
        public String nombres { get; set; }
        public String apellidos { get; set; }
        public String telefono { get; set; }
        public String correo { get; set; }
        public String direccion { get; set; }
        public String observaciones { get; set; }
        public String estado { get; set; }
        public DateTime fechaIngreso { get; set; }
        public DateTime fechaModificacion { get; set; }
        public Int32 usuarioModifica { get; set; }
    }

    public struct ListaPersona
    {
        public String Identificación { get; set; }
        public String Nombre { get; set; }
        public String Telefono { get; set; }
        public String Correo { get; set; }
        public String Dirección { get; set; }
        public String Observaciones { get; set; }
    }
}