namespace BlazingPizza.Model;

public class Order
{
    public int OrderId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTime CreatedTime { get; set; }
    public Address DeliveryAddress { get; set; } = new();
    public List<Pizza> Pizzas { get; set; } = new();

    public decimal GetTotalPrice()
    {
        return Pizzas.Sum(p => p.GetTotalPrice());
    }

    public string GetFormattedTotalPrice()
    {
        return string.Format("${0:0.00}", GetTotalPrice());
    }
}
