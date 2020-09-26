namespace ImpeccableService.Backend.Core.Offering.Model
{
    public class CreateMenuForVenueRequest
    {
        public CreateMenuForVenueRequest(string venueId)
        {
            VenueId = venueId;
        }

        public string VenueId { get; }
    }
}