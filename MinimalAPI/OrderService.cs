using MinimalAPI.Models;

namespace MinimalAPI;

public class OrderService
{
    public List<OrderDto> Orders { get; set; } = new List<OrderDto>()
    {
        new OrderDto()
        {
            OrderId = Guid.NewGuid(),
            CustomerName =  "John Doe",
            CustomerPhoneNumber = "123321"
        }
    };
}