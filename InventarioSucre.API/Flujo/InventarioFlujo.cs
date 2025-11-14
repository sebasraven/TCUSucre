using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class InventarioFlujo : IInventarioFlujo
    {
        private readonly IInventarioDA _inventarioDA;

        public InventarioFlujo(IInventarioDA inventarioDA)
        {
            _inventarioDA = inventarioDA;
        }

        public async Task<Guid> Agregar(InventarioRequest inventario)
        {
            return await _inventarioDA.Agregar(inventario);
        }

        public async Task<Guid> Editar(Guid Id, InventarioRequest inventario)
        {
            return await _inventarioDA.Editar(Id, inventario);
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            return await _inventarioDA.Eliminar(Id);
        }

        public async Task<IEnumerable<InventarioResponse>> Obtener()
        {
            return await _inventarioDA.Obtener();
        }

        public async Task<InventarioResponse?> Obtener(Guid Id)
        {
            return await _inventarioDA.Obtener(Id);
        }
    }
}