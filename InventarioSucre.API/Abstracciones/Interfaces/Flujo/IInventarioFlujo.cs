using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IInventarioFlujo
    {
        Task<IEnumerable<InventarioResponse>> Obtener();
        Task<InventarioResponse?> Obtener(Guid Id);
        Task<Guid> Agregar(InventarioRequest inventario);
        Task<Guid> Editar(Guid Id, InventarioRequest inventario);
        Task<Guid> Eliminar(Guid Id);
    }
}