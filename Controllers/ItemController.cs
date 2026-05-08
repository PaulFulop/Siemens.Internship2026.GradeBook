using Microsoft.AspNetCore.Mvc;
using Siemens.Internship2026.GradeBook.Interfaces;

namespace Siemens.Internship2026.GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        Console.WriteLine($"[LOG] {DateTime.UtcNow}: GET api/item called");

        return Ok(new
        {
            Data = await _itemService.GetAllItemsAsync(),
            Statistics = await _itemService.GetStatisticAsync()
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Console.WriteLine($"[LOG] {DateTime.UtcNow}: GET api/item/{id} called");

        if (id <= 0)
        {
            Console.WriteLine($"[LOG] Invalid id: {id}");
            return BadRequest("Id must be a positive integer.");
        }

        var item = await _itemService.GetByIdAsync(id);
        if (item == null)
        {
            Console.WriteLine($"[LOG] Item {id} not found");
            return NotFound($"Item with Id {id} was not found.");
        }

        return Ok(item);
    }
}
