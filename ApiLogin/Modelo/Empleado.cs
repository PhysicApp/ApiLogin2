using System;
using System.Collections.Generic;

namespace ApiLogin.Models
{
    public partial class Empleado
    {
        public int IdEmpleado { get; set; }
        public string? Nombre { get; set; }
        public string? Departamento { get; set; }
        public double? Sueldo { get; set; }
    }
}
