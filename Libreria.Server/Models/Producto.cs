using System.ComponentModel.DataAnnotations.Schema;

namespace Libreria.Server.Models
{
    public class Producto
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioUnitario { get; set; }

        public int StockActual { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public ICollection<Ingreso> Ingresos { get; set; }
        public ICollection<Salida> Salidas { get; set; }
    }
}
