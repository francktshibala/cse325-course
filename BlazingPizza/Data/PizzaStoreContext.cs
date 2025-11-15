using Microsoft.EntityFrameworkCore;
using BlazingPizza.Model;

namespace BlazingPizza.Data;

public class PizzaStoreContext : DbContext
{
    public PizzaStoreContext(DbContextOptions<PizzaStoreContext> options)
        : base(options)
    {
    }

    public DbSet<PizzaSpecial> Specials { get; set; } = null!;
    public DbSet<Pizza> Pizzas { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Topping> Toppings { get; set; } = null!;
}
