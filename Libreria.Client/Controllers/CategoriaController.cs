using Libreria.Server.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Libreria.Client.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly HttpClient httpClient;

        public CategoriaController(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("LibreriaApi");
            httpClient.BaseAddress = new Uri("https://localhost:7232/");
        }
        public async Task<IActionResult> Index()
        {
            var categorias = await httpClient.GetFromJsonAsync<List<CategoriaDTO>>("api/categorias");
            return View(categorias ?? new List<CategoriaDTO>());
        }
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoriaDTO categoriaDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(categoriaDTO);
            }
            var response = await httpClient.PostAsJsonAsync("api/categorias", categoriaDTO);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            return View(categoriaDTO);
        }
        public async Task<IActionResult> Details(int id)
        {
            var categoria = await httpClient.GetFromJsonAsync<CategoriaDTO>($"api/categorias/{id}");
            if (categoria == null)
                return NotFound();
            return View(categoria);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await httpClient.GetFromJsonAsync<CategoriaDTO>($"api/categorias/{id}");
            if (categoria == null)
                return NotFound();
            return View(categoria);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoriaDTO categoriaDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(categoriaDTO);
            }
            var response = await httpClient.PutAsJsonAsync($"api/categorias/{id}", categoriaDTO);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            return View(categoriaDTO);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await httpClient.GetFromJsonAsync<CategoriaDTO>($"api/categorias/{id}");
            if (categoria == null)
                return NotFound();
            return View(categoria);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await httpClient.GetFromJsonAsync<CategoriaConProductosDTO>($"api/categorias/{id}/productos");
            if(categoria.Productos != null && categoria.Productos.Count > 0)
            {
                ViewBag.ErrorMessage = "No se puede eliminar una categoría que tiene productos asociados.";
                return View(categoria);
            }
            var response = await httpClient.DeleteAsync($"api/categorias/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            return NotFound();
        }
    }
}
