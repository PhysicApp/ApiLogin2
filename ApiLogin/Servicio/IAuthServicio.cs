using ApiLogin.Modelo;

namespace ApiLogin.Servicio
{
    public interface IAuthServicio
    {
        AuthResponse Autenticar(string nombreUsuario, string contrasena);
        public void RegistrarUsuario(UsuarioLogin usuarioRegistro);
    }

}
