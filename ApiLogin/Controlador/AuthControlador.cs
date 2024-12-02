using Microsoft.AspNetCore.Mvc;

namespace ApiLogin.Controlador
{
    using ApiLogin.Modelo.DTOs;
    using ApiLogin.Servicio;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using System.ComponentModel.DataAnnotations;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class AuthControlador : ControllerBase
    {
        private readonly IEmailServicio _emailService;
        private readonly IAuthServicio _authServicio;

        public AuthControlador(IEmailServicio emailService, IAuthServicio authServicio)
        {
            _emailService = emailService;
            _authServicio = authServicio;
        }

        [HttpPost("email-password-reset")]
        public async Task<IActionResult> RequestPasswordReset(EmailRequestDTO email)
        {
            if (string.IsNullOrEmpty(email.Email))
            {
                return BadRequest(new { message = "El correo electrónico es requerido." });
            }

            // Aquí deberías validar el email y generar el token de restablecimiento
            var token = _emailService.GeneratePasswordResetToken(email.Email);  // Implementa tu lógica para generar el token
            var resetLink = $"http://localhost:3000/usuario/restablecerContraseña/{token}";

            string nombreUsuario = "Nombre del Usuario"; // Reemplaza con el nombre del usuario
            string nombreSitio = "Nombre del Sitio"; // Reemplaza con el nombre del sitio
            string correoSoporte = "soporte@ejemplo.com"; // Reemplaza con el correo de soporte

            var body = $@"
        <p>Hola {nombreUsuario},</p>
        <p>Para proteger tu cuenta en {nombreSitio}, hemos generado un enlace para restablecer tu contraseña. 
        Por favor, sigue el enlace a continuación y sigue las instrucciones para crear una nueva contraseña:</p>
        <p><a href='{resetLink}'>Restablecer Contraseña</a></p>
        <p>Si no has solicitado este restablecimiento, por favor ignora este correo y no hagas clic en el enlace.</p>
        <p>Si necesitas más ayuda, no dudes en contactarnos a <a href='mailto:{correoSoporte}'>{correoSoporte}</a>.</p>
        <p>Saludos cordiales,<br>El equipo de {nombreSitio}</p>
    ";

            await _emailService.SendEmailAsync(email.Email, "Restablecer Contraseña", body);

            return Ok(new { message = "Se ha enviado un correo de restablecimiento de contraseña." });
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(RestaurarContraseñaDTOs model)
        {
           string message = await _authServicio.ResetPassword(model);

           return Ok(new { message = message });
        }

        public class EmailRequestDTO { 
            [Required]
            [EmailAddress] 
            public string Email { get; set; }
        }

    }

}
