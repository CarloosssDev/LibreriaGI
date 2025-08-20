
using System.Net;
using Libreria.Server.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libreria.Client.Controllers
{
    public class SalidaController : Controller
    {
        private readonly HttpClient httpClient;
        public SalidaController(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("LibreriaApi");
            httpClient.BaseAddress = new Uri("https://localhost:7232/");
        }
        public async Task<IActionResult> Index()
        {
            var salidas = await httpClient.GetFromJsonAsync<List<SalidaDTO>>("api/salidas");
            return View(salidas);
        }
        public async Task<IActionResult> Details(int id)
        {
            var salida = await httpClient.GetFromJsonAsync<SalidaDTO>($"api/salidas/{id}");
            if (salida == null) return NotFound();
            return View(salida);
        }
        public async Task<IActionResult> Create()
        {
            var productos = await httpClient.GetFromJsonAsync<List<ProductoDTO>>("api/productos");
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SalidaDTO salida)
        {
            var productos = await httpClient.GetFromJsonAsync<List<ProductoDTO>>("api/productos");
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");

            if (ModelState.IsValid)
            {
                var response = await httpClient.PostAsJsonAsync("api/salidas", salida);
                if(response.StatusCode == HttpStatusCode.BadRequest) { 
                    ViewBag.ErrorMessage = "No hay suficiente stock para realizar la salida.";
                    return View(salida);
                }
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(salida);

        }
        public async Task<IActionResult> Edit(int id)
        {
            var productos = await httpClient.GetFromJsonAsync<List<ProductoDTO>>("api/productos");
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");

            var salida = await httpClient.GetFromJsonAsync<SalidaDTO>($"api/salidas/{id}");

            if (salida == null) return NotFound();

            return View(salida);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, SalidaDTO salida)
        {
            var productos = await httpClient.GetFromJsonAsync<List<ProductoDTO>>("api/productos");
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre", salida.ProductoId);

            if (ModelState.IsValid)
            {
                var response = await httpClient.PutAsJsonAsync($"api/salidas/{id}", salida);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    ViewBag.ErrorMessage = "No hay suficiente stock para realizar la salida.";
                    return View(salida);
                }
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Error al actualizar la salida");
            }
            return View(salida);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var salida = await httpClient.GetFromJsonAsync<SalidaDTO>($"api/salidas/{id}");
            if (salida == null) return NotFound();
            return View(salida);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await httpClient.DeleteAsync($"api/salidas/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
