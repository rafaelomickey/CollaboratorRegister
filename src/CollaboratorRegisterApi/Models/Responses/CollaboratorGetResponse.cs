using System.Collections.Generic;

namespace CollaboratorRegisterApi.Models.Responses
{
    public class CollaboratorGetResponse : BaseResponse
    {
        public IEnumerable<CollaboratorResponse> Collaborators { get; set; }
    }
}
