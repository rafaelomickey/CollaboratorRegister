using CollaboratorRegisterApi.Models.Requests;
using CollaboratorRegisterApi.Models.Responses;
using System.Threading.Tasks;

namespace CollaboratorRegisterApi.Interfaces.Services
{
    public interface ICollaboratorService
    {
        Task<BaseResponse> Add(CollaboratorAddRequest request);
        Task<CollaboratorGetResponse> Get(CollaboratorGetRequest collaboratorGetRequest);
        Task<BaseResponse> Delete(int collaboratorId);
        Task<BaseResponse> Update(CollaboratorUpdateRequest request);
    }
}
