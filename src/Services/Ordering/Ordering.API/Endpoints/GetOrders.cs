﻿using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints;

//- Accepts pagination parameters
//- Constructs a `GetOrdersQuery` with the pagination parameters
//- Retrieves the data and returns it in a paginated format

//public record GetOrdersRequest(PaginationRequest PaginationRequest);
public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders",
            async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var query = new GetOrdersQuery(request);
                var result = await sender.Send(query);
                var response = result.Adapt<GetOrdersResponse>();
                return Results.Ok(response);
            })
            .WithName(nameof(GetOrders))
            .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Orders")
            .WithDescription("Get Orders");
    }
}
