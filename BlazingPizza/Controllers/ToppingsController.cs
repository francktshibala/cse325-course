using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazingPizza.Data;
using BlazingPizza.Model;

namespace BlazingPizza.Controllers;

[Route("toppings")]
[ApiController]
public class ToppingsController : ControllerBase
{
    private readonly PizzaStoreContext _db;

    public ToppingsController(PizzaStoreContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<Topping>>> GetToppings()
    {
        return await _db.Toppings.OrderBy(t => t.Name).ToListAsync();
    }
}
