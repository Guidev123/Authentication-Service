namespace Authenticate.API.Endpoints
{
    public interface IEndpointBase
    {
        static abstract void Map(IEndpointRouteBuilder app);
    }
}
