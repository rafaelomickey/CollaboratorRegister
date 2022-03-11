using CollaboratorRegisterApi.Models.Entities;
using CollaboratorRegisterApi.Models.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollaboratorRegisterApi.Interfaces.Repositories
{
    public interface ICollaboratorRepository
    {
        Task<int> Add(Collaborator request);
        Task<bool> Exists(Collaborator request, int? ignoreCollaboratorId = null);
        Task<IEnumerable<Collaborator>> Get(CollaboratorGetRequest request);
        Task Update(Collaborator request);
        Task Delete(int collaboratorId);
    }
}
