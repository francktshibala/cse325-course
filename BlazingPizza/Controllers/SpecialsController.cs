using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazingPizza.Data;
using BlazingPizza.Model;

namespace BlazingPizza.Controllers;

[Route("specials")]
[ApiController]
public class SpecialsController : ControllerBase
{
    private readonly PizzaStoreContext _db;

    public SpecialsController(PizzaStoreContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<PizzaSpecial>>> GetSpecials()
    {
        return await _db.Specials
            .OrderByDescending(s => s.BasePrice)
            .ToListAsync();
    }
}
