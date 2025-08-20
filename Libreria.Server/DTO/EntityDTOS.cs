namespace Libreria.Server.DTO
{
    public class CategoriaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }

    public class CategoriaConProductosDTO : CategoriaDTO
    {
        public List<ProductoDTO> Productos { get; set; } = new List<ProductoDTO>();
    }

    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int StockActual { get; set; }
        public int CategoriaId { get; set; }
        public string? CategoriaNombre { get; set; }
    }

    public class IngresoDTO
    {
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        public int ProductoId { get; set; }
        public string? ProductoNombre { get; set; }
        public int Cantidad { get; set; }
        public string Comentario { get; set; } = string.Empty;
    }

    public class SalidaDTO
    {
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        public int ProductoId { get; set; }
        public string? ProductoNombre { get; set; }
        public int Cantidad { get; set; }
        public string Motivo { get; set; } = string.Empty;
    }
}
