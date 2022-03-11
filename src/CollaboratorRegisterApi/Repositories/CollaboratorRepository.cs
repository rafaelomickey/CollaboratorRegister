using CollaboratorRegisterApi.Interfaces.Repositories;
using CollaboratorRegisterApi.Models.Entities;
using CollaboratorRegisterApi.Models.Requests;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace CollaboratorRegisterApi.Repositories
{
    [ExcludeFromCodeCoverage]
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private readonly string _connectionString;

        public CollaboratorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> Add(Collaborator request)
        {
            var query = @"INSERT INTO TB_COLLABORATOR
                        (
                            CLB_NAME,
                            CLB_MAIL,
                            CLB_PLATE_NUMBER,
                            CLB_PASSWORD,
                            CLB_LEADER_ID
                        ) 
                        VALUES
                        (
                            @Name,
                            @Mail,
                            @PlateNumber,
                            @Password,
                            @LeaderId
                        ); SELECT SCOPE_IDENTITY();";

            await using var conn = new SqlConnection(_connectionString);
            if (conn.State == ConnectionState.Closed)
                await conn.OpenAsync();

            return await conn.ExecuteScalarAsync<int>(query, request);
        }

        public async Task Update(Collaborator request)
        {
            var query = @"UPDATE TB_COLLABORATOR
                          SET CLB_NAME = @Name,
                              CLB_MAIL = @Mail,
                              CLB_PLATE_NUMBER = @PlateNumber,
                              CLB_PASSWORD = @Password,
                              CLB_LEADER_ID = @LeaderId
                          WHERE CLB_ID = @Id";

            await using var conn = new SqlConnection(_connectionString);
            if (conn.State == ConnectionState.Closed)
                await conn.OpenAsync();

            await conn.ExecuteScalarAsync(query, request);
        }

        public async Task Delete(int collaboratorId)
        {
            var query = @"DELETE FROM TB_COLLABORATOR WHERE CLB_ID = @Id";

            await using var conn = new SqlConnection(_connectionString);
            if (conn.State == ConnectionState.Closed)
                await conn.OpenAsync();

            await conn.ExecuteScalarAsync(query, new { Id = collaboratorId});
        }

        public async Task<bool> Exists(Collaborator request, int? ignoreCollaboratorId = null)
        {
            var query = @"SELECT 
                            TOP 1 1 
                          FROM TB_COLLABORATOR 
                          WHERE CLB_PLATE_NUMBER = @PlateNumber
                          AND (CLB_ID <> @IgnoreCollaboratorId OR @IgnoreCollaboratorId IS NULL)";

            await using var conn = new SqlConnection(_connectionString);
            if (conn.State == ConnectionState.Closed)
                await conn.OpenAsync();

            return await conn.ExecuteScalarAsync<bool>(query, new
            {
                PlateNumber = request.PlateNumber,
                IgnoreCollaboratorId = ignoreCollaboratorId
            });
        }

        public async Task<IEnumerable<Collaborator>> Get(CollaboratorGetRequest request)
        {
            var query = @"SELECT 
                            CLB_ID Id,
                            CLB_NAME Name,
                            CLB_MAIL Mail,
                            CLB_PLATE_NUMBER PlateNumber,
                            CLB_PASSWORD Password,
                            CLB_LEADER_ID LeaderId
                          FROM TB_COLLABORATOR 
                          /**where**/";

            var builder = new SqlBuilder();

            if (request.Id != null)
                builder.Where("CLB_ID = @Id", new { Id = request.Id });

            if (!string.IsNullOrEmpty(request.PlateNumber))
                builder.Where("CLB_PLATE_NUMBER = @PlateNumber", new { PlateNumber = request.PlateNumber });

            if (request.LeaderId != null)
                builder.Where("CLB_LEADER_ID = @LeaderId", new { LeaderId = request.LeaderId });

            var selector = builder.AddTemplate(query);

            await using var conn = new SqlConnection(_connectionString);
            if (conn.State == ConnectionState.Closed)
                await conn.OpenAsync();

            return await conn.QueryAsync<Collaborator>(selector.RawSql, selector.Parameters);
        }
    }
}
