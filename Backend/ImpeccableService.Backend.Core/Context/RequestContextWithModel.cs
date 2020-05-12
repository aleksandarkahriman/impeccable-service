namespace ImpeccableService.Backend.Core.Context
{
    public class RequestContextWithModel<T> : RequestContext
    {
        public RequestContextWithModel(T model, Identity identity = null) : base(identity)
        {
            Model = model;
        }

        public T Model { get; }
    }
}
