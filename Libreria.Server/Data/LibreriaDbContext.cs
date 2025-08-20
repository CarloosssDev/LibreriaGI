using Microsoft.EntityFrameworkCore;

namespace Libreria.Server.Data
{
    public class LibreriaDbContext : DbContext
    {
        public LibreriaDbContext(DbContextOptions<LibreriaDbContext> options) : base(options) { }
        public DbSet<Models.Producto> Productos { get; set; }
        public DbSet<Models.Categoria> Categorias { get; set; }
        public DbSet<Models.Ingreso> Ingresos { get; set; }
        public DbSet<Models.Salida> Salidas { get; set; }

    }
}
