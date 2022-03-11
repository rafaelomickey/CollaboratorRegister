using CollaboratorRegisterApi.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollaboratorRegisterApi.Interfaces.Repositories
{
    public interface ICollaboratorPhoneRepository
    {
        Task Add(int collaboratorId, CollaboratorPhone request);
        Task<IEnumerable<CollaboratorPhone>> Get(int collaboratorId);
        Task Delete(int collaboratorId);
    }
}
