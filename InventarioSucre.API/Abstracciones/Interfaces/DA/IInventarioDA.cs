using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface IInventarioDA
    {
        Task<IEnumerable<InventarioResponse>> Obtener();
        Task<InventarioResponse?> Obtener(Guid Id);
        Task<Guid> Agregar(InventarioRequest inventario);
        Task<Guid> Editar(Guid Id, InventarioRequest inventario);
        Task<Guid> Eliminar(Guid Id);
    }
}