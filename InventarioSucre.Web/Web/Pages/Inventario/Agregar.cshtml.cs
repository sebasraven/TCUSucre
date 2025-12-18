using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace Web.Pages.Inventario
{
    public class AgregarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public InventarioRequest Inventario { get; set; } = new InventarioRequest();

        [BindProperty]
        public List<SelectListItem> Estados { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGet()
        {
            await ObtenerEstados();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                await ObtenerEstados();
                return Page();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarInventario");
            var cliente = new HttpClient();
            var respuesta = await cliente.PostAsJsonAsync(endpoint, Inventario);
            respuesta.EnsureSuccessStatusCode();

            // Limpiar el formulario para seguir agregando
            ModelState.Clear();
            Inventario = new InventarioRequest();
            await ObtenerEstados();

            ViewData["Message"] = "Bien registrado correctamente. Puedes seguir agregando más.";
            return Page();
        }

        private async Task ObtenerEstados()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerEstados");
            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endpoint);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var estados = JsonSerializer.Deserialize<List<EstadoResponse>>(resultado, opciones);

            Estados = estados.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.NombreEstado
            }).ToList();
        }
    }
}