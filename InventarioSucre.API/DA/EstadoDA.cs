using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class EstadoDA : IEstadoDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public EstadoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region Operaciones

        public async Task<IEnumerable<EstadoResponse>> Obtener()
        {
            string query = @"ObtenerEstados"; // Stored Procedure en tu BD
            var resultadoConsulta = await _sqlConnection.QueryAsync<EstadoResponse>(query);
            return resultadoConsulta;
        }

        public async Task<EstadoResponse?> Obtener(Guid Id)
        {
            string query = @"ObtenerEstado"; // Stored Procedure en tu BD
            var resultadoConsulta = await _sqlConnection.QueryAsync<EstadoResponse>(query, new { Id });
            return resultadoConsulta.FirstOrDefault();
        }

        public async Task<Guid> Agregar(EstadoRequest estado)
        {
            string query = @"AgregarEstado"; // Stored Procedure en tu BD
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Guid.NewGuid(),
                NombreEstado = estado.NombreEstado
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, EstadoRequest estado)
        {
            await VerificarEstadoExiste(Id);
            string query = @"EditarEstado"; // Stored Procedure en tu BD
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id,
                NombreEstado = estado.NombreEstado
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await VerificarEstadoExiste(Id);
            string query = @"EliminarEstado"; // Stored Procedure en tu BD
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new { Id });
            return resultadoConsulta;
        }

        #endregion

        #region Helpers

        private async Task VerificarEstadoExiste(Guid Id)
        {
            var resultadoConsultaEstado = await Obtener(Id);
            if (resultadoConsultaEstado == null)
                throw new Exception("Estado no encontrado.");
        }

        #endregion
    }
}