using Libreria.Server.DTO;
using Libreria.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libreria.Client.Controllers
{
    public class ProductoController : Controller
    {
        private readonly HttpClient httpClient;

        public ProductoController(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("LibreriaApi");
            httpClient.BaseAddress = new Uri("https://localhost:7232/");
        }

        // ------------------- LISTADO -------------------
        public async Task<IActionResult> Index()
        {
            var productos = await httpClient.GetFromJsonAsync<List<ProductoDTO>>("api/productos");
            return View(productos ?? new List<ProductoDTO>());
        }

        // ------------------- CREAR -------------------
        public async Task<IActionResult> Create()
        {
            var categorias = await httpClient.GetFromJsonAsync<List<CategoriaDTO>>("api/categorias");
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductoDTO productoDTO)
        {
            var categorias = await httpClient.GetFromJsonAsync<List<CategoriaDTO>>("api/categorias");

            if (!ModelState.IsValid)
            {
                ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre");

                return View(productoDTO);
            }


            var response = await httpClient.PostAsJsonAsync("api/productos", productoDTO);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre");
            return View(productoDTO);
        }

        // ------------------- DETALLES -------------------
        public async Task<IActionResult> Details(int id)
        {
            var producto = await httpClient.GetFromJsonAsync<ProductoDTO>($"api/productos/{id}");
            if (producto == null) return NotFound();
            return View(producto);
        }

        // ------------------- EDITAR -------------------
        public async Task<IActionResult> Edit(int id)
        {
            var producto = await httpClient.GetFromJsonAsync<ProductoDTO>($"api/productos/{id}");
            if (producto == null) return NotFound();

            var categorias = await httpClient.GetFromJsonAsync<List<CategoriaDTO>>("api/categorias");
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductoDTO productoDTO)
        {
            var categorias = await httpClient.GetFromJsonAsync<List<CategoriaDTO>>("api/categorias");
            if (!ModelState.IsValid)
            {
                ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre");

                return View(productoDTO);
            }

            var response = await httpClient.PutAsJsonAsync($"api/productos/{id}", productoDTO);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre", productoDTO.CategoriaId);
            return View(productoDTO);
        }

        // ------------------- ELIMINAR -------------------
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await httpClient.GetFromJsonAsync<ProductoDTO>($"api/productos/{id}");
            if (producto == null) return NotFound();
            return View(producto);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await httpClient.DeleteAsync($"api/productos/{id}");
            return RedirectToAction("Index");
        }
    }
}
