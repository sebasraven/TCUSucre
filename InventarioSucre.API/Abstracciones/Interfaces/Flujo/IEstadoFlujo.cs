using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IEstadoFlujo
    {
        Task<IEnumerable<EstadoResponse>> Obtener();
        Task<EstadoResponse?> Obtener(Guid Id);
        Task<Guid> Agregar(EstadoRequest estado);
        Task<Guid> Editar(Guid Id, EstadoRequest estado);
        Task<Guid> Eliminar(Guid Id);
    }
}