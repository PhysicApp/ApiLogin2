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
            var token = _authServicio.Autenticar(usuario.Username, usuario.Password);

            if (token == null)
                return Unauthorized();

            return Ok(new { Token = token });
        }
    }

}
