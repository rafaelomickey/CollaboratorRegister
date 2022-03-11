using CollaboratorRegisterApi.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollaboratorRegisterApi.Interfaces.Services
{
    public interface ICollaboratorPhoneService
    {
        Task Add(int collaboratorId, IEnumerable<CollaboratorPhone> phones);
        Task<IEnumerable<CollaboratorPhone>> Get(int collaboratorId);
        Task Delete(int collaboratorId);
    }
}
