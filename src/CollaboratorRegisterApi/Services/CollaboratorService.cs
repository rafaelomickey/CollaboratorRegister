using AutoMapper;
using CollaboratorRegisterApi.Interfaces.Repositories;
using CollaboratorRegisterApi.Interfaces.Services;
using CollaboratorRegisterApi.Models.Entities;
using CollaboratorRegisterApi.Models.Requests;
using CollaboratorRegisterApi.Models.Responses;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollaboratorRegisterApi.Services
{
    public class CollaboratorService : ICollaboratorService
    {
        private readonly IMapper _mapper;
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly ICollaboratorPhoneService _collaboratorPhoneService;

        public CollaboratorService(
            IMapper mapper,
            ICollaboratorPhoneService collaboratorPhoneService,
            ICollaboratorRepository collaboratorRepository
        )
        {
            _mapper = mapper;
            _collaboratorRepository = collaboratorRepository;
            _collaboratorPhoneService = collaboratorPhoneService;
        }

        public async Task<CollaboratorGetResponse> Get(CollaboratorGetRequest collaboratorGetRequest)
        {
            var collaborators = await _collaboratorRepository.Get(collaboratorGetRequest);

            foreach (var collaborator in collaborators)
            {
                var phones = await _collaboratorPhoneService.Get(collaborator.Id);
                collaborator.Phones = phones;
            }

            return _mapper.Map<CollaboratorGetResponse>(collaborators);
        }

        public async Task<BaseResponse> Delete(int collaboratorId)
        {
            var isValidObject = await DeleteValidate(collaboratorId);
            if (isValidObject != null)
                return isValidObject;

            await _collaboratorPhoneService.Delete(collaboratorId);
            await _collaboratorRepository.Delete(collaboratorId);

            return new BaseResponse { StatusCode = StatusCodes.Status200OK };
        }

        public async Task<BaseResponse> Update(CollaboratorUpdateRequest request)
        {
            var domain = _mapper.Map<Collaborator>(request);

            var isValidObject = await SaveValidate(domain, true);
            if (isValidObject != null)
                return isValidObject;

            await _collaboratorRepository.Update(domain);
            await _collaboratorPhoneService.Delete(domain.Id);
            await _collaboratorPhoneService.Add(domain.Id, domain.Phones);

            return new BaseResponse { StatusCode = StatusCodes.Status200OK };
        }

        public async Task<BaseResponse> Add(CollaboratorAddRequest request)
        {
            var domain = _mapper.Map<Collaborator>(request);

            var isValidObject = await SaveValidate(domain);
            if (isValidObject != null)
                return isValidObject;

            var newCollaboratorId = await _collaboratorRepository.Add(domain);
            await _collaboratorPhoneService.Add(newCollaboratorId, domain.Phones);

            return new BaseResponse { StatusCode = StatusCodes.Status201Created, CreatedId = newCollaboratorId };
        }

        private async Task<BaseResponse> SaveValidate(Collaborator request, bool ignoreActualCollaborator = false)
        {
            var ignoreCollaboratorId = ignoreActualCollaborator ? request.Id : (int?)null;
            var plateNumberExists = await _collaboratorRepository.Exists(request, ignoreCollaboratorId);
            if (plateNumberExists)
                return new BaseResponse { StatusCode = StatusCodes.Status409Conflict, ErrorMessage = string.Format(Resource.AlreadyExists, Resource.PlateNumber) };

            if (request.LeaderId.HasValue)
            {
                var leaderCheck = await _collaboratorRepository.Get(new CollaboratorGetRequest { Id = request.LeaderId });
                if (leaderCheck == null || !leaderCheck.Any())
                    return new BaseResponse { StatusCode = StatusCodes.Status409Conflict, ErrorMessage = string.Format(Resource.NotExists, Resource.Leader) };
            }

            return null;
        }

        private async Task<BaseResponse> DeleteValidate(int collaboratorId)
        {
            var collaborators = await _collaboratorRepository.Get(new CollaboratorGetRequest { LeaderId = collaboratorId });
            if (collaborators != null && collaborators.Any())
                return new BaseResponse { StatusCode = StatusCodes.Status409Conflict, ErrorMessage = string.Format(Resource.InUse, Resource.Leader) };

            return null;
        }
    }
}
