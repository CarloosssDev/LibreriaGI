using Libreria.Server.Data;
using Libreria.Server.DTO;
using Libreria.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Libreria.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresosController : ControllerBase
    {
        private readonly LibreriaDbContext _context;
        public IngresosController(LibreriaDbContext context) => _context = context;

        [HttpGet]
        public IActionResult GetAll() => Ok(_context.Ingresos.Include(i => i.Producto).Select(MapToDto).ToList());

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var ingreso = _context.Ingresos.Include(i => i.Producto).FirstOrDefault(i => i.Id == id);
            if (ingreso == null) return NotFound();
            return Ok(MapToDto(ingreso));
        }

        [HttpPost]
        public IActionResult Create(IngresoDTO dto)
        {
            var producto = _context.Productos.Find(dto.ProductoId);
            if (producto == null) return BadRequest("Producto inexistente");

            var ingreso = new Ingreso
            {
                ProductoId = dto.ProductoId,
                Cantidad = dto.Cantidad,
                Comentario = dto.Comentario,
                Fecha = DateTime.Now
            };
            producto.StockActual += dto.Cantidad;
            _context.Ingresos.Add(ingreso);
            _context.SaveChanges();
            return Ok(MapToDto(ingreso));
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, IngresoDTO dto)
        {
            var ingreso = _context.Ingresos.Include(i => i.Producto).FirstOrDefault(i => i.Id == id);
            if (ingreso == null) return NotFound();

            var productoNuevo = _context.Productos.Find(dto.ProductoId);
            if (productoNuevo == null) return BadRequest("Producto inexistente");

            var productoAnterior = _context.Productos.Find(ingreso.ProductoId);


            if (productoNuevo.Id != productoAnterior.Id)
            {
                productoAnterior.StockActual -= ingreso.Cantidad;
                productoNuevo.StockActual += dto.Cantidad;    
            }
            else if (ingreso.Cantidad != dto.Cantidad)
            {
                productoNuevo.StockActual += dto.Cantidad - ingreso.Cantidad;
            }

            ingreso.ProductoId = dto.ProductoId;
            ingreso.Cantidad = dto.Cantidad;
            ingreso.Comentario = dto.Comentario;

            _context.SaveChanges();
            return Ok(MapToDto(ingreso));
        }


        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var ingreso = _context.Ingresos.Include(i => i.Producto).FirstOrDefault(i => i.Id == id);
            if (ingreso == null) return NotFound();
            var producto = _context.Productos.Find(ingreso.ProductoId);
            producto.StockActual -= ingreso.Cantidad;
            _context.Ingresos.Remove(ingreso);
            _context.SaveChanges();
            return NoContent();
        }

        private IngresoDTO MapToDto(Ingreso ingreso)
        {
            return new IngresoDTO
            {
                Id = ingreso.Id,
                Fecha = ingreso.Fecha,
                ProductoId = ingreso.ProductoId,
                ProductoNombre = ingreso.Producto.Nombre,
                Cantidad = ingreso.Cantidad,
                Comentario = ingreso.Comentario
            };
        }
    }

}
