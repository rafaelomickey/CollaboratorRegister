using CollaboratorRegisterApi.Interfaces.Repositories;
using CollaboratorRegisterApi.Models.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace CollaboratorRegisterApi.Repositories
{
    [ExcludeFromCodeCoverage]
    public class CollaboratorPhoneRepository : ICollaboratorPhoneRepository
    {
        private readonly string _connectionString;

        public CollaboratorPhoneRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Add(int collaboratorId, CollaboratorPhone request)
        {
            var query = @"INSERT INTO TB_COLLABORATOR_PHONE
                        (
                            CLP_CLB_ID,
                            CLP_DESCRIPTION,
                            CLP_NUMBER
                        ) 
                        VALUES
                        (
                            @CollaboratorId,
                            @Description,
                            @Number
                        );";

            await using var conn = new SqlConnection(_connectionString);
            if (conn.State == ConnectionState.Closed)
                await conn.OpenAsync();

            await conn.ExecuteScalarAsync(query, new
            {
                CollaboratorId = collaboratorId,
                Description = request.Description,
                Number = request.Number
            });
        }

        public async Task<IEnumerable<CollaboratorPhone>> Get(int collaboratorId)
        {
            var query = @"SELECT
                            CLP_ID Id,
                            CLP_DESCRIPTION Description,
                            CLP_NUMBER Number
                          FROM TB_COLLABORATOR_PHONE
                          WHERE CLP_CLB_ID = @CollaboratorId";

            await using var conn = new SqlConnection(_connectionString);
            if (conn.State == ConnectionState.Closed)
                await conn.OpenAsync();

            return await conn.QueryAsync<CollaboratorPhone>(query, new { CollaboratorId = collaboratorId });
        }

        public async Task Delete(int collaboratorId)
        {
            var query = @"DELETE FROM TB_COLLABORATOR_PHONE WHERE CLP_CLB_ID = @CollaboratorId";

            await using var conn = new SqlConnection(_connectionString);
            if (conn.State == ConnectionState.Closed)
                await conn.OpenAsync();

            await conn.ExecuteAsync(query, new { CollaboratorId = collaboratorId });
        }
    }
}
