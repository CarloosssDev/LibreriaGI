using Libreria.Server.Data;
using Libreria.Server.DTO;
using Libreria.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Libreria.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly LibreriaDbContext _context;
        public CategoriasController(LibreriaDbContext context) => _context = context;

        [HttpGet]
        public IActionResult GetAll() => Ok(_context.Categorias.Select(MapToDto).ToList());

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var categoria = _context.Categorias.Find(id);
            if (categoria == null) return NotFound();
            return Ok(MapToDto(categoria));
        }

        [HttpGet("{id:int}/productos")]
        public IActionResult GetByIdWhitProducts(int id)
        {
            var categoria = _context.Categorias.Include(c => c.Productos).FirstOrDefault(c => c.Id == id);
            if(categoria == null) return NotFound();

            return Ok(new CategoriaConProductosDTO
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
                Productos = categoria.Productos.Select(p => new ProductoDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    PrecioUnitario = p.PrecioUnitario,
                    StockActual = p.StockActual,
                    CategoriaId = p.CategoriaId,
                    CategoriaNombre = p.Categoria?.Nombre
                }).ToList()
            }); 
        }

        [HttpPost]
        public IActionResult Create(CategoriaDTO dto)
        {
            var categoria = new Categoria { Nombre = dto.Nombre };
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return Ok(MapToDto(categoria));
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, CategoriaDTO dto)
        {
            var categoria = _context.Categorias.Find(id);
            if (categoria == null) return NotFound();
            categoria.Nombre = dto.Nombre;
            categoria.Descripcion = dto.Descripcion;
            _context.SaveChanges();
            return Ok(MapToDto(categoria));
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var categoria = _context.Categorias.Find(id);
            if (categoria == null) return NotFound();
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return NoContent();
        }

        private CategoriaDTO MapToDto(Categoria categoria)
        {
            return new CategoriaDTO {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion
            };
        }
    }
}