using System;
using System.Collections.Generic;

namespace ApiLogin.Modelo
{
    public partial class UsuarioLogin
    {
        public long IdLogin { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
    }
}
