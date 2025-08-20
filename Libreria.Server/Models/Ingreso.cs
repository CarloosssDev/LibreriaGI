namespace Libreria.Server.Models
{
    public class Ingreso
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; } 

        public int ProductoId { get; set; }

        public Producto Producto { get; set; }

        public int Cantidad { get; set; }

        public string? Comentario { get; set; }
    }
}
