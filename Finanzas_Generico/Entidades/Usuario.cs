﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzas_Generico.Entidades
{
    class Usuario
    {
        public String id { get; set; }
        public String identificacion { get; set; }
        public String nick { get; set; }
        public String pass { get; set; }
        public String nivelPermiso { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaModificacion { get; set; }
        public DateTime usuarioModifica { get; set; }
    }
}