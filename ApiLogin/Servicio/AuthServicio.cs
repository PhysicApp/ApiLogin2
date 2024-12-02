namespace ApiLogin.Servicio
{
    using ApiLogin.Repositorio;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using BCrypt.Net;
    using ApiLogin.Modelo;
    using ApiLogin.Modelo.DTOs;
    using Microsoft.AspNetCore.Mvc;

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

        public async Task<string> ResetPassword(RestaurarContraseñaDTOs model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuracion["Jwt:Key"]);
            SecurityToken validatedToken;
            string message;

            try
            {
                var principal = tokenHandler.ValidateToken(model.Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero // Elimina el tiempo de tolerancia
                }, out validatedToken);

                var emailClaim = principal.FindFirst("email")?.Value;
                if (emailClaim == null)
                {
                    message = "Token inválido";

                    return message;
                }

                // Aquí deberías actualizar la contraseña del usuario en tu base de datos
                UsuarioLogin usuarioLogin = new UsuarioLogin {
                    Username = emailClaim,
                    Password = model.NewPassword              
                };

                _usuarioRepositorio.GuardarUsuario(usuarioLogin);

                return message = "Contraseña restablecida con éxito";
            }
            catch (Exception ex)
            {
                return message = "Token inválido";
            }
        }

    }
}
