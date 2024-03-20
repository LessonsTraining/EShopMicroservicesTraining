﻿using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints;

//- Accepts a name parameter
//- Constructs a `GetOrdersByNameQuery` with the name parameter
//- Retrieves the data and returns matching orders

// public record GetOrdersByNameRequest(string Name);
public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderName}",
            async (string orderName, ISender sender) =>
            {
                var query = new GetOrdersByNameQuery(orderName);
                var result = await sender.Send(query);
                var response = result.Adapt<GetOrdersByNameResponse>();
                return Results.Ok(response);
            })
            .WithName(nameof(GetOrdersByName))
            .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Orders By Name")
            .WithDescription("Get Orders By Name");
    }
}
