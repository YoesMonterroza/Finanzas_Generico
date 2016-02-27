﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzas_Generico.Entidades
{
    class Persona
    {
        public String id { get; set; }
        public String identificacion { get; set; }
        public String nombres { get; set; }
        public String apellidos { get; set; }
        public String telefono { get; set; }
        public String correo { get; set; }
        public String direccion { get; set; }
        public DateTime fechaIngreso { get; set; }
        public DateTime fechaModificacion { get; set; }
        public String usuarioModifica { get; set; }
    }
}