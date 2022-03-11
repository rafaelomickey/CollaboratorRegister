using System.Collections.Generic;

namespace CollaboratorRegisterApi.Models.Entities
{
    public class Collaborator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string PlateNumber { get; set; } 
        public IEnumerable<CollaboratorPhone> Phones { get; set; }
        public string Password { get; set; }
        public int? LeaderId { get; set; }
    }
}
