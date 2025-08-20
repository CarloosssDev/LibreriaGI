namespace Libreria.Server.Models
{
    public class Salida
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public int ProductoId { get; set; }

        public Producto Producto { get; set; }

        public int Cantidad { get; set; }

        public string Motivo { get; set; }
    }
}
