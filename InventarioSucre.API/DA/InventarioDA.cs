using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class InventarioDA : IInventarioDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public InventarioDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region Operaciones

        public async Task<Guid> Agregar(InventarioRequest inventario)
        {
            string query = @"AgregarInventario";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Guid.NewGuid(),
                NumeroIdentificacion = inventario.NumeroIdentificacion,
                Descripcion = inventario.Descripcion,
                Marca = inventario.Marca,
                Modelo = inventario.Modelo,
                Serie = inventario.Serie,
                IdEstado = inventario.IdEstado,
                Ubicacion = inventario.Ubicacion,
                ModoAdquisicion = inventario.ModoAdquisicion,
                Observaciones = inventario.Observaciones,
                ObservacionesInstitucionales = inventario.ObservacionesInstitucionales
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, InventarioRequest inventario)
        {
            await VerificarInventarioExiste(Id);
            string query = @"EditarInventario";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id,
                NumeroIdentificacion = inventario.NumeroIdentificacion,
                Descripcion = inventario.Descripcion,
                Marca = inventario.Marca,
                Modelo = inventario.Modelo,
                Serie = inventario.Serie,
                IdEstado = inventario.IdEstado,
                Ubicacion = inventario.Ubicacion,
                ModoAdquisicion = inventario.ModoAdquisicion,
                Observaciones = inventario.Observaciones,
                ObservacionesInstitucionales = inventario.ObservacionesInstitucionales
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await VerificarInventarioExiste(Id);
            string query = @"EliminarInventario";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new { Id });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<InventarioResponse>> Obtener()
        {
            string query = @"ObtenerInventarios";
            var resultadoConsulta = await _sqlConnection.QueryAsync<InventarioResponse>(query);
            return resultadoConsulta;
        }

        public async Task<InventarioResponse?> Obtener(Guid Id)
        {
            string query = @"ObtenerInventario";
            var resultadoConsulta = await _sqlConnection.QueryAsync<InventarioResponse>(query, new { Id });
            return resultadoConsulta.FirstOrDefault();
        }

        #endregion

        #region Helpers

        private async Task VerificarInventarioExiste(Guid Id)
        {
            var resultadoConsultaInventario = await Obtener(Id);
            if (resultadoConsultaInventario == null)
                throw new Exception("Inventario no encontrado.");
        }

        #endregion
    }
}