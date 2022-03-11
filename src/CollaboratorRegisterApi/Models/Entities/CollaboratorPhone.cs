namespace CollaboratorRegisterApi.Models.Entities
{
    public class CollaboratorPhone
    {
        public int Id { get; set; }
        public int CollaboratorId { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
    }
}
