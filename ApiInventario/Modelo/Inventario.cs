using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiInventario.Modelo
{

    public class Inventario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdInventario { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaAdquisicion { get; set; }

        public Inventario()
        {
        }

        public Inventario(int idInventario, string nombre, string descripcion, int cantidad, decimal precio, DateTime fechaAdquisicion)
        {
            IdInventario = idInventario;
            Nombre = nombre;
            Descripcion = descripcion;
            Cantidad = cantidad;
            Precio = precio;
            FechaAdquisicion = fechaAdquisicion;
        }

        public override string ToString()
        {
            return $"IdInventario: {IdInventario}, Nombre: {Nombre}, Descripción: {Descripcion}, Cantidad: {Cantidad}, Precio: {Precio:C}, Fecha de Adquisición: {FechaAdquisicion:d}";
        }

    }

}
