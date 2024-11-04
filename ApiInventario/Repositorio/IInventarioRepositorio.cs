using ApiInventario.Modelo;
using System.Linq;

namespace ApiInventario.Repositorio
{
    public interface IInventarioRepositorio
    {
        Task<List<Inventario>> ObtenerInventario();
        Task<Inventario> ObtenerInventarioPorId(int idInventario);
        Task CrearInventario(Inventario inventario);
        Task ActualizarInventario(Inventario inventario);
        Task EliminarInventario(int idInventario);

    }
}
