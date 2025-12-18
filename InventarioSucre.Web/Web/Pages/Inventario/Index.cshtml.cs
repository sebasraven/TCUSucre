using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace Web.Pages.Inventario
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public IList<InventarioResponse> inventarios { get; set; } = new List<InventarioResponse>();

        [BindProperty(SupportsGet = true)]
        public string? FiltroTexto { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? FiltroEstado { get; set; }

        public List<SelectListItem> Estados { get; set; } = new();

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet()
        {
            await ObtenerEstados();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerInventarios");
            var client = new HttpClient();
            var respuesta = await client.GetAsync(endpoint);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var lista = JsonSerializer.Deserialize<List<InventarioResponse>>(resultado, opciones) ?? new List<InventarioResponse>();

            if (!string.IsNullOrEmpty(FiltroTexto))
            {
                lista = lista.Where(i =>
                    (!string.IsNullOrEmpty(i.NumeroIdentificacion) && i.NumeroIdentificacion.Contains(FiltroTexto, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(i.Descripcion) && i.Descripcion.Contains(FiltroTexto, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(i.Marca) && i.Marca.Contains(FiltroTexto, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(i.Modelo) && i.Modelo.Contains(FiltroTexto, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(i.Serie) && i.Serie.Contains(FiltroTexto, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(i.Estado) && i.Estado.Contains(FiltroTexto, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(i.Ubicacion) && i.Ubicacion.Contains(FiltroTexto, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(i.ModoAdquisicion) && i.ModoAdquisicion.Contains(FiltroTexto, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(i.Observaciones) && i.Observaciones.Contains(FiltroTexto, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(i.ObservacionesInstitucionales) && i.ObservacionesInstitucionales.Contains(FiltroTexto, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            if (!string.IsNullOrEmpty(FiltroEstado))
            {
                lista = lista.Where(i => i.Estado.Equals(FiltroEstado, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            inventarios = lista;
        }

        private async Task ObtenerEstados()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerEstados");
            var client = new HttpClient();
            var respuesta = await client.GetAsync(endpoint);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var estados = JsonSerializer.Deserialize<List<EstadoResponse>>(resultado, opciones) ?? new List<EstadoResponse>();

            Estados = estados.Select(e => new SelectListItem
            {
                Value = e.NombreEstado,
                Text = e.NombreEstado
            }).ToList();
        }
    }
}