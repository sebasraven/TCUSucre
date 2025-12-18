using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.ComponentModel;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase, IInventarioController
    {
        private readonly IInventarioFlujo _inventarioFlujo;
        private readonly ILogger<InventarioController> _logger;

        public InventarioController(IInventarioFlujo inventarioFlujo, ILogger<InventarioController> logger)
        {
            _inventarioFlujo = inventarioFlujo;
            _logger = logger;
        }

        #region Operaciones

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] InventarioRequest inventario)
        {
            var resultado = await _inventarioFlujo.Agregar(inventario);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Editar([FromRoute] Guid Id, [FromBody] InventarioRequest inventario)
        {
            if (!await VerificarInventarioExiste(Id))
                return NotFound("El inventario no existe");

            var resultado = await _inventarioFlujo.Editar(Id, inventario);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid Id)
        {
            if (!await VerificarInventarioExiste(Id))
                return NotFound("El inventario no existe");

            var resultado = await _inventarioFlujo.Eliminar(Id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _inventarioFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid Id)
        {
            var resultado = await _inventarioFlujo.Obtener(Id);
            if (resultado == null)
                return NotFound("El inventario no existe");

            return Ok(resultado);
        }

        [HttpGet("Exportar")]
        public async Task<IActionResult> Exportar()
        {
            var inventarios = await _inventarioFlujo.Obtener();

            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Inventario");

            // Encabezados
            worksheet.Cells[1, 1].Value = "N° de identif.";
            worksheet.Cells[1, 2].Value = "Descripción";
            worksheet.Cells[1, 3].Value = "Marca";
            worksheet.Cells[1, 4].Value = "Modelo";
            worksheet.Cells[1, 5].Value = "Serie";
            worksheet.Cells[1, 6].Value = "Estado";
            worksheet.Cells[1, 7].Value = "Ubicación";
            worksheet.Cells[1, 8].Value = "Modo de Adquisición";
            worksheet.Cells[1, 9].Value = "Observaciones";
            worksheet.Cells[1, 10].Value = "Observaciones Institucionales";

            // Datos
            int fila = 2;
            foreach (var inv in inventarios)
            {
                worksheet.Cells[fila, 1].Value = inv.NumeroIdentificacion;
                worksheet.Cells[fila, 2].Value = inv.Descripcion;
                worksheet.Cells[fila, 3].Value = inv.Marca;
                worksheet.Cells[fila, 4].Value = inv.Modelo;
                worksheet.Cells[fila, 5].Value = inv.Serie;
                worksheet.Cells[fila, 6].Value = inv.Estado;
                worksheet.Cells[fila, 7].Value = inv.Ubicacion;
                worksheet.Cells[fila, 8].Value = inv.ModoAdquisicion;
                worksheet.Cells[fila, 9].Value = inv.Observaciones;
                worksheet.Cells[fila, 10].Value = inv.ObservacionesInstitucionales;
                fila++;
            }

            // Aplicar estilos con 50 colores
            AplicarEstilos(worksheet, fila - 1);

            var content = package.GetAsByteArray();
            return File(content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "InventarioEscolar.xlsx");
        }


        #endregion Operaciones

        #region Helpers

        private async Task<bool> VerificarInventarioExiste(Guid Id)
        {
            var resultadoInventarioExiste = await _inventarioFlujo.Obtener(Id);
            return resultadoInventarioExiste != null;
        }



        private void AplicarEstilos(ExcelWorksheet worksheet, int totalFilas)
        {
            int totalColumnas = 10;

            // Encabezados
            var headerRange = worksheet.Cells[1, 1, 1, totalColumnas];
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            headerRange.Style.Fill.BackgroundColor.SetColor(Color.Yellow);
            headerRange.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

            // 🎨 Lista de 50 colores
            var colores = new List<Color>
    {
        Color.LightBlue, Color.LightGreen, Color.MistyRose, Color.LightYellow, Color.LightGray,
        Color.LightPink, Color.LightCyan, Color.LightGoldenrodYellow, Color.LightSalmon, Color.LightSeaGreen,
        Color.LightSkyBlue, Color.LightSlateGray, Color.LightSteelBlue, Color.Lavender, Color.LavenderBlush,
        Color.LemonChiffon, Color.Honeydew, Color.Azure, Color.Beige, Color.Bisque,
        Color.BlanchedAlmond, Color.BurlyWood, Color.CadetBlue, Color.Chartreuse, Color.Coral,
        Color.CornflowerBlue, Color.Cornsilk, Color.DarkSalmon, Color.Gainsboro, Color.GhostWhite,
        Color.Khaki, Color.Moccasin, Color.NavajoWhite, Color.OldLace, Color.PapayaWhip,
        Color.PeachPuff, Color.PaleGoldenrod, Color.PaleGreen, Color.PaleTurquoise, Color.PaleVioletRed,
        Color.PowderBlue, Color.RosyBrown, Color.SeaShell, Color.Thistle, Color.Tan,
        Color.Wheat, Color.WhiteSmoke, Color.YellowGreen, Color.SandyBrown, Color.Aquamarine
    };

            // Detectar ubicaciones distintas
            var ubicaciones = new HashSet<string>();
            for (int fila = 2; fila <= totalFilas; fila++)
            {
                ubicaciones.Add(worksheet.Cells[fila, 7].Text);
            }

            // Asignar color a cada ubicación
            var ubicacionColores = new Dictionary<string, Color>();
            int index = 0;
            foreach (var ubicacion in ubicaciones)
            {
                ubicacionColores[ubicacion] = colores[index % colores.Count];
                index++;
            }

            // Aplicar color por fila según ubicación
            for (int fila = 2; fila <= totalFilas; fila++)
            {
                string ubicacion = worksheet.Cells[fila, 7].Text;
                if (ubicacionColores.ContainsKey(ubicacion))
                {
                    var rowRange = worksheet.Cells[fila, 1, fila, totalColumnas];
                    rowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rowRange.Style.Fill.BackgroundColor.SetColor(ubicacionColores[ubicacion]);

                    rowRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    rowRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    rowRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    rowRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    rowRange.Style.Border.Top.Color.SetColor(Color.Black);
                    rowRange.Style.Border.Bottom.Color.SetColor(Color.Black);
                    rowRange.Style.Border.Left.Color.SetColor(Color.Black);
                    rowRange.Style.Border.Right.Color.SetColor(Color.Black);
                }
            }

            // Ajustar ancho de columnas
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
        }


        #endregion Helpers
    }
}