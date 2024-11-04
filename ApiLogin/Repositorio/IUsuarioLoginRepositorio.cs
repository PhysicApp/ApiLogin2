using ApiLogin.Modelo;

namespace ApiLogin.Repositorio
{
    public interface IUsuarioLoginRepositorio
    {
        UsuarioLogin ObtenerUsuarioPorNombre(string nombreUsuario);
        void GuardarUsuario(UsuarioLogin usuarioLogin);
    }
}
