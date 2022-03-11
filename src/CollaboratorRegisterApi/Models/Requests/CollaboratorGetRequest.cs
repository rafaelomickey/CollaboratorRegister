namespace CollaboratorRegisterApi.Models.Requests
{
    public class CollaboratorGetRequest
    {
        public int? Id { get; set; }
        public int? LeaderId { get; set; }
        public string PlateNumber { get; set; }
    }
}
