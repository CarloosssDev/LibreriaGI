using Libreria.Server.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libreria.Client.Controllers
{
    public class IngresoController : Controller
    {
        private readonly HttpClient httpClient;
        public IngresoController(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("LibreriaApi");
            httpClient.BaseAddress = new Uri("https://localhost:7232/");
        }
        public async Task<IActionResult> Index()
        {
            var ingresos = await httpClient.GetFromJsonAsync<List<IngresoDTO>>("api/ingresos");
            return View(ingresos);
        }
        public async Task<IActionResult> Create()
        {
            var productos = await httpClient.GetFromJsonAsync<List<ProductoDTO>>("api/productos");

            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(IngresoDTO ingresoDTO)
        {
            var productos = await httpClient.GetFromJsonAsync<List<ProductoDTO>>("api/productos");
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");

            Console.WriteLine(ingresoDTO.Fecha);

            if (!ModelState.IsValid)
            {
                return View(ingresoDTO);
            }

            var response = await httpClient.PostAsJsonAsync("api/ingresos", ingresoDTO);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(ingresoDTO);
        }
        public async Task<IActionResult> Details(int id)
        {
            var ingreso = await httpClient.GetFromJsonAsync<IngresoDTO>($"api/ingresos/{id}");
            if (ingreso == null) return NotFound();
            return View(ingreso);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var ingreso = await httpClient.GetFromJsonAsync<IngresoDTO>($"api/ingresos/{id}");
            if (ingreso == null) return NotFound();
            var productos = await httpClient.GetFromJsonAsync<List<ProductoDTO>>("api/productos");
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre", ingreso.ProductoId);
            return View(ingreso);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, IngresoDTO ingresoDTO)
        {
            var productos = await httpClient.GetFromJsonAsync<List<ProductoDTO>>("api/productos");
            if (!ModelState.IsValid)
            {
                ViewBag.Productos = new SelectList(productos, "Id", "Nombre");
                return View(ingresoDTO);
            }
            var response = await httpClient.PutAsJsonAsync($"api/ingresos/{id}", ingresoDTO);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Productos = new SelectList(productos, "Id", "Nombre", ingresoDTO.ProductoId);
            return View(ingresoDTO);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var ingreso = await httpClient.GetFromJsonAsync<IngresoDTO>($"api/ingresos/{id}");
            if (ingreso == null) return NotFound();
            return View(ingreso);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await httpClient.DeleteAsync($"api/ingresos/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            return NotFound();
        }
    }
}