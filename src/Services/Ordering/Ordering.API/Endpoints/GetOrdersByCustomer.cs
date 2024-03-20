﻿using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints;

//- Accepts a customer ID
//- Uses a `GetOrdersByCustomerQuery` to fetch the orders
//- Returns the list of orders for the customer

// public record GetOrdersByCustomerRequest(Guid CustomerId);
public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{customerId}",
                async (Guid customerId, ISender sender) =>
                {
                    var query = new GetOrdersByCustomerQuery(customerId);
                    var result = await sender.Send(query);
                    var response = result.Adapt<GetOrdersByCustomerResponse>();
                    return Results.Ok(response);
                })
            .WithName(nameof(GetOrdersByCustomer))
            .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Orders By Customer")
            .WithDescription("Get Orders By Customer");
    }
}
