using Microsoft.AspNetCore.Routing;

namespace DungeonDelvers.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}