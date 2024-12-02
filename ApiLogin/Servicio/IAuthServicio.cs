using ApiLogin.Modelo;
using ApiLogin.Modelo.DTOs;

namespace ApiLogin.Servicio
{
    public interface IAuthServicio
    {
        AuthResponse Autenticar(string nombreUsuario, string contrasena);
        public void RegistrarUsuario(UsuarioLogin usuarioRegistro);
        public Task<string> ResetPassword(RestaurarContraseñaDTOs model);
    }

}
