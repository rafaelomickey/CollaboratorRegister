using System.Collections.Generic;

namespace CollaboratorRegisterApi.Models.Requests
{
    public class CollaboratorUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string PlateNumber { get; set; }
        public IEnumerable<CollaboratorPhoneAddRequest> Phones { get; set; }
        public string Password { get; set; }
        public int? LeaderId { get; set; }
    }
}
