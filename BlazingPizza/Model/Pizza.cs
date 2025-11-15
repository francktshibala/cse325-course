namespace BlazingPizza.Model;

public class Pizza
{
    public const int DefaultSize = 12;
    public const int MinimumSize = 9;
    public const int MaximumSize = 17;

    public int Id { get; set; }
    public int OrderId { get; set; }
    public PizzaSpecial? Special { get; set; }
    public int SpecialId { get; set; }
    public int Size { get; set; }
    public List<PizzaTopping> Toppings { get; set; } = new();

    public decimal GetBasePrice()
    {
        return Special?.BasePrice ?? 0;
    }

    public decimal GetTotalPrice()
    {
        return ((GetBasePrice() + Toppings.Sum(t => t.Topping?.Price ?? 0)) / DefaultSize) * Size;
    }

    public string GetFormattedTotalPrice()
    {
        return string.Format("${0:0.00}", GetTotalPrice());
    }
}
