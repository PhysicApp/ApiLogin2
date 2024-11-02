using ApiInventario.Modelo;
using Microsoft.EntityFrameworkCore;

namespace ApiInventario.Data
{

    public class InventarioContext : DbContext
    {


        public InventarioContext(DbContextOptions<InventarioContext> options)
            : base(options)
        {
        }

        public DbSet<Inventario> Inventarios { get; set; }


    }
}

