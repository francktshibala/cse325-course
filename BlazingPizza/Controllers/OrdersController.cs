using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazingPizza.Data;
using BlazingPizza.Model;

namespace BlazingPizza.Controllers;

[Route("orders")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly PizzaStoreContext _db;

    public OrdersController(PizzaStoreContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<ActionResult<int>> PlaceOrder(Order order)
    {
        order.CreatedTime = DateTime.Now;
        order.UserId = "default-user";

        foreach (var pizza in order.Pizzas)
        {
            pizza.SpecialId = pizza.Special!.Id;
            pizza.Special = null;

            foreach (var topping in pizza.Toppings)
            {
                topping.ToppingId = topping.Topping!.Id;
                topping.Topping = null;
            }
        }

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        return order.OrderId;
    }

    [HttpGet("{orderId}")]
    public async Task<ActionResult<OrderWithStatus>> GetOrderWithStatus(int orderId)
    {
        var order = await _db.Orders
            .Where(o => o.OrderId == orderId)
            .Include(o => o.DeliveryAddress)
            .Include(o => o.Pizzas).ThenInclude(p => p.Special)
            .Include(o => o.Pizzas).ThenInclude(p => p.Toppings).ThenInclude(t => t.Topping)
            .FirstOrDefaultAsync();

        if (order == null)
        {
            return NotFound();
        }

        return OrderWithStatus.FromOrder(order);
    }
}
