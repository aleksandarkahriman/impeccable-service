namespace ImpeccableService.Backend.Core.Context
{
    public class RequestContext<T>
    {
        public RequestContext(T model)
        {
            Model = model;
        }

        public T Model { get; }
    }
}
