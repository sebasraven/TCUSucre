using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface IEstadoDA
    {
        Task<IEnumerable<EstadoResponse>> Obtener();
        Task<EstadoResponse?> Obtener(Guid Id);
        Task<Guid> Agregar(EstadoRequest estado);
        Task<Guid> Editar(Guid Id, EstadoRequest estado);
        Task<Guid> Eliminar(Guid Id);
    }
}