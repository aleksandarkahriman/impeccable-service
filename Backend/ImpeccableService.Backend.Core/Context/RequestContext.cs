namespace ImpeccableService.Backend.Core.Context
{
    public class RequestContext
    {
        public RequestContext(Identity identity)
        {
            Identity = identity;
        }

        public Identity Identity { get; }
    }
}
