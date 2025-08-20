using Libreria.Server.Data;
using Libreria.Server.DTO;
using Libreria.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Libreria.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidasController : ControllerBase
    {
        private readonly LibreriaDbContext _context;
        public SalidasController(LibreriaDbContext context) => _context = context;

        [HttpGet]
        public IActionResult GetAll() => Ok(_context.Salidas.Include(s => s.Producto).Select(MapToDto).ToList());

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var salida = _context.Salidas.Include(i => i.Producto).FirstOrDefault(i => i.Id == id);
            if (salida == null) return NotFound();
            return Ok(MapToDto(salida));
        }

        [HttpPost]
        public IActionResult Create(SalidaDTO dto)
        {
            var producto = _context.Productos.Find(dto.ProductoId);
            if (producto == null) return BadRequest("Producto inexistente");

            if(producto.StockActual < dto.Cantidad)
                return BadRequest("No hay suficiente stock para realizar la salida");

            var salida = new Salida
            {
                ProductoId = dto.ProductoId,
                Cantidad = dto.Cantidad,
                Motivo = dto.Motivo,
                Fecha = DateTime.Now
            };
            producto.StockActual -= dto.Cantidad;
            _context.Salidas.Add(salida);
            _context.SaveChanges();
            return Ok(MapToDto(salida));
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, SalidaDTO dto)
        {
            var salida = _context.Salidas.Include(s => s.Producto).FirstOrDefault(s => s.Id == id);
            if (salida == null) return NotFound();

            var productoNuevo = _context.Productos.Find(dto.ProductoId);
            if (productoNuevo == null) return BadRequest("Producto inexistente");

            if (productoNuevo.StockActual < dto.Cantidad)
                return BadRequest("No hay suficiente stock para realizar la salida");

            var productoAnterior = _context.Productos.Find(salida.ProductoId);

            if (productoNuevo.Id != productoAnterior.Id)
            {
                productoAnterior.StockActual += salida.Cantidad;

                productoNuevo.StockActual -= dto.Cantidad;
            }
            else if (salida.Cantidad != dto.Cantidad)
            {
                productoNuevo.StockActual += salida.Cantidad;
                productoNuevo.StockActual -= dto.Cantidad;
            }

            salida.ProductoId = dto.ProductoId;
            salida.Cantidad = dto.Cantidad;
            salida.Motivo = dto.Motivo;

            _context.SaveChanges();
            return Ok(MapToDto(salida));
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var salida = _context.Salidas.Find(id);
            if (salida == null) return NotFound();
            var producto = _context.Productos.Find(salida.ProductoId);
            producto.StockActual += salida.Cantidad;
            _context.Salidas.Remove(salida);
            _context.SaveChanges();
            return NoContent();
        }

        private SalidaDTO MapToDto(Salida salida)
        {
            return new SalidaDTO
            {
                Id = salida.Id,
                ProductoId = salida.ProductoId,
                ProductoNombre = salida.Producto.Nombre,
                Cantidad = salida.Cantidad,
                Fecha = salida.Fecha,
                Motivo = salida.Motivo
            };
        }
    }
}
