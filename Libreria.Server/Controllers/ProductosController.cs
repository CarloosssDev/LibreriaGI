using Libreria.Server.Data;
using Libreria.Server.DTO;
using Libreria.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Libreria.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly LibreriaDbContext _context;
        public ProductosController(LibreriaDbContext context) => _context = context;

        [HttpGet]
        public IActionResult GetAll()
        {
            var productos = _context.Productos
                .Include(p => p.Categoria)
                .Select(MapToDto)
                .ToList();
            return Ok(productos);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var producto = _context.Productos.Include(p => p.Categoria).FirstOrDefault(p => p.Id == id);
            if (producto == null) return NotFound();
            return Ok(MapToDto(producto));
        }

        [HttpPost]
        public IActionResult Create(ProductoDTO dto)
        {
            var categoria = _context.Categorias.Find(dto.CategoriaId);
            Console.WriteLine($"CategoriaId: {dto.CategoriaId}");
            if (categoria == null) return BadRequest("Se necesita una categoria existente");

            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                PrecioUnitario = dto.PrecioUnitario,
                StockActual = dto.StockActual,
                CategoriaId = dto.CategoriaId
            };
            _context.Productos.Add(producto);
            _context.SaveChanges();
            producto = _context.Productos.Include(p => p.Categoria).First(p => p.Id == producto.Id);
            return Ok(MapToDto(producto));
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, ProductoDTO dto)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null) return NotFound();

            var categoria = _context.Categorias.Find(dto.CategoriaId);
            if (categoria == null) return BadRequest("Se necesita una categoria existente");

            producto.Nombre = dto.Nombre;
            producto.Descripcion = dto.Descripcion;
            producto.PrecioUnitario = dto.PrecioUnitario;
            producto.StockActual = dto.StockActual;
            producto.CategoriaId = dto.CategoriaId;

            _context.SaveChanges();
            producto = _context.Productos.Include(p => p.Categoria).First(p => p.Id == id);
            return Ok(MapToDto(producto));
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null) return NotFound();
            _context.Productos.Remove(producto);
            _context.SaveChanges();
            return NoContent();
        }

        // ------------------- Método MapToDto -------------------
        private ProductoDTO MapToDto(Producto producto)
        {
            return new ProductoDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                PrecioUnitario = producto.PrecioUnitario,
                StockActual = producto.StockActual,
                CategoriaId = producto.CategoriaId,
                CategoriaNombre = producto.Categoria?.Nombre    
            };
        }
    }
}