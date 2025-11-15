namespace BlazingPizza.Model;

public class OrderWithStatus
{
    public readonly static TimeSpan PreparationDuration = TimeSpan.FromSeconds(10);
    public readonly static TimeSpan DeliveryDuration = TimeSpan.FromMinutes(1);

    public Order Order { get; set; } = null!;

    public string StatusText
    {
        get
        {
            var dispatchTime = Order.CreatedTime.Add(PreparationDuration);

            if (DateTime.Now < dispatchTime)
            {
                return "Preparing";
            }
            else if (DateTime.Now < dispatchTime + DeliveryDuration)
            {
                return "Out for delivery";
            }
            else
            {
                return "Delivered";
            }
        }
    }

    public static OrderWithStatus FromOrder(Order order) => new OrderWithStatus { Order = order };
}
