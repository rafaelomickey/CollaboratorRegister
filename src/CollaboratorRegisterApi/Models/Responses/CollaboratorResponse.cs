using System.Collections.Generic;

namespace CollaboratorRegisterApi.Models.Responses
{
    public class CollaboratorResponse 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string PlateNumber { get; set; }
        public IEnumerable<CollaboratorPhoneResponse> Phones { get; set; }
        public int? LeaderId { get; set; }
    }
}
