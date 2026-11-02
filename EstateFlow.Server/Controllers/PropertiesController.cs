using Microsoft.AspNetCore.Mvc;
using EstateFlow.Server.Data;
using EstateFlow.Server.Domain;
using Microsoft.EntityFrameworkCore;



namespace EstateFlow.Server.Controllers;


[Route("api/[controller]")]

[ApiController]
public class PropertiesController : ControllerBase { 

    private readonly EstateFlowDbContext _context;

    public PropertiesController(EstateFlowDbContext context)
    {
        _context = context;
    }

    // GET: api/Properties
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Property>>> GetProperties()
    {
        var properties = await _context.Properties.ToListAsync();
        return Ok(properties);
    }

    // GET: api/Properties/{id}
    [HttpGet("{id}")] 
    public async Task<ActionResult<Property>> GetProperty(Guid id)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property is null)
        {
            return NotFound();
        }
        return Ok(property);
    }
}


