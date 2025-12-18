using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Inventario
{
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public InventarioResponse Inventario { get; set; } = default!;

        [BindProperty]
        public Guid EstadoSeleccionado { get; set; } 

        [BindProperty]
        public List<SelectListItem> Estados { get; set; } = new List<SelectListItem>();

        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            // Obtener Inventario por Id
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerInventario");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                Inventario = JsonSerializer.Deserialize<InventarioResponse>(resultado, opciones)!;
            }

            // Llenar dropdown de estados
            await ObtenerEstados();

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                await ObtenerEstados();
                return Page();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarInventario");
            var cliente = new HttpClient();
            var respuesta = await cliente.PutAsJsonAsync(string.Format(endpoint, Inventario.Id), new InventarioRequest
            {
                NumeroIdentificacion = Inventario.NumeroIdentificacion,
                Descripcion = Inventario.Descripcion,
                Marca = Inventario.Marca,
                Modelo = Inventario.Modelo,
                Serie = Inventario.Serie,
                IdEstado = EstadoSeleccionado,
                Ubicacion = Inventario.Ubicacion,
                ModoAdquisicion = Inventario.ModoAdquisicion,
                Observaciones = Inventario.Observaciones,
                ObservacionesInstitucionales = Inventario.ObservacionesInstitucionales
            });

            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
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
                Text = e.NombreEstado,
                Selected = (Inventario != null && Inventario.Estado == e.NombreEstado)
            }).ToList();

            // Preseleccionar el estado actual
            var estadoActual = estados.FirstOrDefault(e => e.NombreEstado == Inventario.Estado);
            if (estadoActual != null)
                EstadoSeleccionado = estadoActual.Id;
        }
    }
}