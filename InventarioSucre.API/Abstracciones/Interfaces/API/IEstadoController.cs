using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API
{
    public interface IEstadoController
    {
        Task<IActionResult> Obtener();
    }
}