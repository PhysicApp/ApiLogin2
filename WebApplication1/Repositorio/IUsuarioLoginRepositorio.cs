using ApiLogin.Modelo;

namespace ApiLogin.Repositorio
{
    public interface IUsuarioLoginRepositorio
    {
        UsuarioLogin ObtenerUsuarioPorNombre(string nombreUsuario);
    }
}
