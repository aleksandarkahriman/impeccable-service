namespace ImpeccableService.Backend.Core.Offering.Model
{
    public class GetMenuForVenueRequest
    {
        public GetMenuForVenueRequest(string venueId)
        {
            VenueId = venueId;
        }

        public string VenueId { get; }
    }
}