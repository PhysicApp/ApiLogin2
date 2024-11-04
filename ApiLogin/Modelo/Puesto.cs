using System;
using System.Collections.Generic;

namespace ApiLogin.Modelo
{
    public partial class Puesto
    {
        public int IdPuesto { get; set; }
        public string? Abreviacion { get; set; }
        public ulong? Activo { get; set; }
        public string? NombrePuesto { get; set; }
    }
}
