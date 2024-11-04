using ApiInventario.Modelo;
using ApiInventario.Repositorio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiInventario.Servicio
{
    public class InventarioServicio : IInventarioServicio
    {
        private readonly IInventarioRepositorio _inventarioRepositorio;

        public InventarioServicio(IInventarioRepositorio inventarioRepositorio)
        {
            _inventarioRepositorio = inventarioRepositorio;
        }

        public async Task<string> ActualizarInventario(Inventario inventario)
        {
            try
            {
                var resultado = await _inventarioRepositorio.ObtenerInventarioPorId(inventario.IdInventario);

                if(resultado != null)
                {
                    await _inventarioRepositorio.ActualizarInventario(inventario);
                    return "Inventario actualizado exitosamente";
                }
                else
                {
                    return "El producto con ID:" + inventario.IdInventario + "no existe";
                }
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                Console.WriteLine($"Error al actualizar el inventario: {ex.Message}");
                return $"Error al actualizar el inventario: {ex.Message}";
            }
        }

        public async Task<List<Inventario>> ConsultarInventario()
        {
            try
            {
                return await _inventarioRepositorio.ObtenerInventario();
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                Console.WriteLine($"Error al consultar el inventario: {ex.Message}");
                throw;
            }
        }

        public async Task<Inventario> ConsultarInventarioPorId(int idInventario)
        {
            try
            {
                return await _inventarioRepositorio.ObtenerInventarioPorId(idInventario);
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                Console.WriteLine($"Error al consultar el inventario por ID: {ex.Message}");
                throw;
            }
        }

        public async Task<string> CrearInventario(Inventario inventario)
        {
            try
            {
                await _inventarioRepositorio.CrearInventario(inventario);
                return "Inventario creado exitosamente";
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                Console.WriteLine($"Error al crear el inventario: {ex.Message}");
                return $"Error al crear el inventario: {ex.Message}";
            }
        }

        public async Task<string> EliminarInventario(int idInventario)
        {
            try
            {
                await _inventarioRepositorio.EliminarInventario(idInventario);
                return "Inventario eliminado exitosamente";
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                Console.WriteLine($"Error al eliminar el inventario: {ex.Message}");
                return $"Error al eliminar el inventario: {ex.Message}";
            }
        }
    }
}
