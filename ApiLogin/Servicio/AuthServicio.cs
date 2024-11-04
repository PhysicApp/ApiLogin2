namespace ApiLogin.Servicio
{
    using ApiLogin.Repositorio;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using BCrypt.Net;
    using ApiLogin.Modelo;

    public class AuthServicio : IAuthServicio
    {
        private readonly IUsuarioLoginRepositorio _usuarioRepositorio;
        private readonly IConfiguration _configuracion;

        public AuthServicio(IUsuarioLoginRepositorio usuarioLoginRepositorio, IConfiguration configuracion)
        {
            _usuarioRepositorio = usuarioLoginRepositorio;
            _configuracion = configuracion;
        }

        public string HashPassword(string password)
        {
            // Genera un hash seguro para la contraseña
            return BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            // Verifica si la contraseña coincide con el hash almacenado
            return BCrypt.Verify(password, hashedPassword);
        }

        public AuthResponse Autenticar(string nombreUsuario, string contrasena)
        {
            var usuario = _usuarioRepositorio.ObtenerUsuarioPorNombre(nombreUsuario);

            if (usuario == null || !BCrypt.Verify(contrasena, usuario.Password))
                return null!;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuracion["Jwt:Key"]);

            // Configurar la zona horaria de CDMX
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"); // Para UTC-6:00 CDMX
            var localTime = DateTime.UtcNow; // Obtener la hora actual en UTC
            var cdmxTime = TimeZoneInfo.ConvertTimeFromUtc(localTime, timeZone); // Convertir la hora UTC a CST

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Username!)
                }),
                NotBefore = cdmxTime,
                Expires = cdmxTime.AddMinutes(30), // Ajusta la fecha de expiración a tu zona horaria
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResponse
            {
                Token = tokenHandler.WriteToken(token),
                Expiracion = tokenDescriptor.Expires.Value
            };
        }



        public void RegistrarUsuario(UsuarioLogin usuarioRegistro)
        {
            var hashedPassword = BCrypt.HashPassword(usuarioRegistro.Password);
            var usuario = new UsuarioLogin { Username = usuarioRegistro.Username, Password = hashedPassword };
            _usuarioRepositorio.GuardarUsuario(usuario);
        }
    }
}
