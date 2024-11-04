namespace ApiLogin.Servicio
{
    using ApiLogin.Repositorio;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class AuthServicio : IAuthServicio
    {
        private readonly IUsuarioLoginRepositorio _usuarioRepositorio;
        private readonly IConfiguration _configuracion;

        public AuthServicio(IUsuarioLoginRepositorio usuarioLoginRepositorio, IConfiguration configuracion)
        {
            _usuarioRepositorio = usuarioLoginRepositorio;
            _configuracion = configuracion;
        }

        public string Autenticar(string nombreUsuario, string contrasena)
        {
            var usuario = _usuarioRepositorio.ObtenerUsuarioPorNombre(nombreUsuario);

            if (usuario == null || usuario.Password != contrasena)
                return null!;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuracion["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, usuario.Username!)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}
