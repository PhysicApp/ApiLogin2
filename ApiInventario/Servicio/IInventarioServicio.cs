using ApiInventario.Modelo;

namespace ApiInventario.Servicio
{
    public interface IInventarioServicio
    {
        Task<List<Inventario>> ConsultarInventario();
        Task<Inventario> ConsultarInventarioPorId(int idInventario);
        Task<string> CrearInventario(Inventario inventario);
        Task<string> ActualizarInventario(Inventario inventario);
        Task<string> EliminarInventario(int idInventario);
    }
}
