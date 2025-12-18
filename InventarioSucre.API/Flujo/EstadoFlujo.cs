using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class EstadoFlujo : IEstadoFlujo
    {
        private readonly IEstadoDA _estadoDA;

        public EstadoFlujo(IEstadoDA estadoDA)
        {
            _estadoDA = estadoDA;
        }

        public async Task<Guid> Agregar(EstadoRequest estado)
        {
            return await _estadoDA.Agregar(estado);
        }

        public async Task<Guid> Editar(Guid Id, EstadoRequest estado)
        {
            return await _estadoDA.Editar(Id, estado);
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            return await _estadoDA.Eliminar(Id);
        }

        public async Task<IEnumerable<EstadoResponse>> Obtener()
        {
            return await _estadoDA.Obtener();
        }

        public async Task<EstadoResponse?> Obtener(Guid Id)
        {
            return await _estadoDA.Obtener(Id);
        }
    }
}