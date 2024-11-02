using ApiLogin.Data;
using ApiLogin.Modelo;

namespace ApiLogin.Repositorio
{
    public class UsuarioLoginRepositorio : IUsuarioLoginRepositorio
    {
        private readonly RecursosHumanosContext _context;

        public UsuarioLoginRepositorio(RecursosHumanosContext context)
        {
            _context = context;
        }

        public UsuarioLogin ObtenerUsuarioPorNombre(string nombreUsuario)
        {
            var resultado = _context.UsuarioLogins.FirstOrDefault(u => u.Username == nombreUsuario);

            return resultado;
        }
    }

}
