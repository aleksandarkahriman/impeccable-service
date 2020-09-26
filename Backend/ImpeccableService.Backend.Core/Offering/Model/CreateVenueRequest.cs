namespace ImpeccableService.Backend.Core.Offering.Model
{
    public class CreateVenueRequest
    {
        public CreateVenueRequest(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}