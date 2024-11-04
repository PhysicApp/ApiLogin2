using Microsoft.AspNetCore.Mvc;

namespace ApiLogin.Controlador
{
    using ApiLogin.Modelo;
    using ApiLogin.Servicio;
    using Microsoft.AspNetCore.Mvc;

    [Route("rh-app/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServicio _authServicio;

        public AuthController(IAuthServicio authServicio)
        {
            _authServicio = authServicio;
        }

        [HttpPost("/login")]
        public IActionResult Login([FromBody] UsuarioLogin usuario)
        {
            var authResponse = _authServicio.Autenticar(usuario.Username, usuario.Password);

            if (authResponse.Token == null)
                return Unauthorized();

            return Ok(authResponse);
        }

        [HttpPost("/registrar")]
        public IActionResult RegistarUsuario(UsuarioLogin usuario)
        {
            _authServicio.RegistrarUsuario(usuario);

            return Ok(new {mensaje = "Usuario registrado exitosamente" });
        }
    }

}
