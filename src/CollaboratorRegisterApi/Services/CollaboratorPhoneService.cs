using CollaboratorRegisterApi.Interfaces.Repositories;
using CollaboratorRegisterApi.Interfaces.Services;
using CollaboratorRegisterApi.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollaboratorRegisterApi.Services
{
    public class CollaboratorPhoneService : ICollaboratorPhoneService
    {
        private readonly ICollaboratorPhoneRepository _collaboratorPhoneRepository;

        public CollaboratorPhoneService(ICollaboratorPhoneRepository collaboratorPhoneRepository)
        {
            _collaboratorPhoneRepository = collaboratorPhoneRepository;
        }

        public async Task<IEnumerable<CollaboratorPhone>> Get(int collaboratorId)
        {
            return await _collaboratorPhoneRepository.Get(collaboratorId);
        }

        public async Task Add(int collaboratorId, IEnumerable<CollaboratorPhone> phones)
        {
            foreach (var phone in phones)
                await _collaboratorPhoneRepository.Add(collaboratorId, phone);
        }

        public async Task Delete(int collaboratorId)
        {
            await _collaboratorPhoneRepository.Delete(collaboratorId);
        }
    }
}
