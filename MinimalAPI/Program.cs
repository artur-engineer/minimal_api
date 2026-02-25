using Microsoft.OpenApi;
using MinimalAPI;
using MinimalAPI.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<OrderService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Customers API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("v1/swagger.json", "Customers API V1"); });
}

app.MapGet("/orders", (OrderService orderService) =>
{
    var orders = orderService.Orders;
    return Results.Ok(orders);
});

app.MapGet("/orders/{orderId}", (Guid orderId, OrderService orderService) =>
{
    var order = orderService.Orders.SingleOrDefault(w => w.OrderId == orderId);

    if (order == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(order);
});

app.MapPost("/orders", (OrderDto order, OrderService orderService) =>
{
    order.OrderId = Guid.NewGuid();
    orderService.Orders.Add(order);
    return Results.Created($"/orders/{order.OrderId}", order);
});

app.MapPut("/orders/{orderId}", (Guid orderId, OrderDto order, OrderService orderService) =>
{
    var existOrder = orderService.Orders.SingleOrDefault(w => w.OrderId == orderId);

    if (existOrder == null)
    {
        return Results.NotFound();
    }

    if (!string.IsNullOrEmpty(order.CustomerName))
    {
        existOrder.CustomerName = order.CustomerName;
    }
    
    if (!string.IsNullOrEmpty(order.CustomerPhoneNumber))
    {
        existOrder.CustomerPhoneNumber = order.CustomerPhoneNumber;
    }

    return Results.Ok(existOrder);
});

app.MapDelete("/orders/{orderId}", (Guid orderId, OrderService orderService) =>
{
    var existOrder = orderService.Orders.SingleOrDefault(w => w.OrderId == orderId);

    if (existOrder == null)
    {
        return Results.NotFound();
    }
    
    orderService.Orders.Remove(existOrder);
    return Results.NoContent();
});

app.Run();