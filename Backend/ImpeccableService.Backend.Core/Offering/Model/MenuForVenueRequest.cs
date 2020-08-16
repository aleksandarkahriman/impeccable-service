namespace ImpeccableService.Backend.Core.Offering.Model
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