namespace ImpeccableService.Backend.Domain.Offering
{
    public class MenuForVenueRequest
    {
        public MenuForVenueRequest(string venueId)
        {
            VenueId = venueId;
        }

        public string VenueId { get; }
    }
}