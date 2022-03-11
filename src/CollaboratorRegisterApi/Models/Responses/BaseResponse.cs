using System.Text.Json.Serialization;

namespace CollaboratorRegisterApi.Models.Responses
{
    public class BaseResponse
    {        
        public int? CreatedId { get; set; }
        public string ErrorMessage { get; set; }
        
        [JsonIgnore]
        public int StatusCode { get; set; }
    }
}
