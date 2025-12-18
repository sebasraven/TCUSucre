using Abstracciones.Interfaces.Reglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;

namespace Web.Pages.Inventario
{
    public class ExportarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public ExportarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<IActionResult> OnGet()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ExportarInventario");

            using var client = new HttpClient();
            var response = await client.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
                return NotFound("No se pudo generar el archivo Excel");

            var content = await response.Content.ReadAsByteArrayAsync();

            return File(content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "InventarioWeb.xlsx");
        }
    }
}