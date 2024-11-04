using ApiInventario.Data;
using ApiInventario.Modelo;
using Microsoft.EntityFrameworkCore;

namespace ApiInventario.Repositorio
{
    public class InventarioRepositorio : IInventarioRepositorio
    {
        private readonly InventarioContext _context;

        public InventarioRepositorio(InventarioContext context)
        {
            _context = context;
        }

        public async Task ActualizarInventario(Inventario inventario)
        {
            try
            {
                _context.Inventarios.Update(inventario);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                Console.WriteLine($"Error al actualizar el inventario: {ex.Message}");
                throw;
            }
        }

        public async Task CrearInventario(Inventario inventario)
        {
            try
            {
                await _context.Inventarios.AddAsync(inventario);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                Console.WriteLine($"Error al crear el inventario: {ex.Message}");
                throw;
            }
        }

        public async Task EliminarInventario(int idInventario)
        {
            try
            {
                var inventario = await _context.Inventarios.FirstAsync(x => x.IdInventario == idInventario);

                if (inventario != null)
                {
                    _context.Inventarios.Remove(inventario);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                Console.WriteLine($"Error al eliminar el inventario: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Inventario>> ObtenerInventario()
        {
            try
            {
                var inventarios = await _context.Inventarios.ToListAsync();
                return inventarios;
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                Console.WriteLine($"Error al obtener inventarios: {ex.Message}");
                throw;
            }
        }

        public async Task<Inventario> ObtenerInventarioPorId(int idInventario)
        {
            try
            {
                Inventario inventario = await _context.Inventarios.FirstAsync(x => x.IdInventario == idInventario);
                return inventario;
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                Console.WriteLine($"Error al obtener el inventario por ID: {ex.Message}");
                throw;
            }
        }
    }
}
