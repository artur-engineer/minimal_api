namespace MinimalAPI.Models;

public class OrderDto
{
    public Guid OrderId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerPhoneNumber { get; set; }
}