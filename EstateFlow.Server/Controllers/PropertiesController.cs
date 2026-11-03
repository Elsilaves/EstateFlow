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
        var properties = await _context.Properties.Include(p=>p.City).ToListAsync();
        return Ok(properties);
    }

    // GET: api/Properties/{id}
    [HttpGet("{id}")] 
    public async Task<ActionResult<Property>> GetProperty(Guid id)
    {
        var property = await _context.Properties.Include(p=>p.City).FirstOrDefaultAsync(p=>p.Id == id);
        if (property is null)
        {
            return NotFound();
        }
        return Ok(property);
    }

    // GET: api/Properties/{id}/meters
    [HttpGet("{id}/meters")]
    public async Task<ActionResult<IEnumerable<Meter>>> GetPropertyMeters(Guid id)
    {
        var propertyExists = await _context.Properties.AnyAsync(p => p.Id == id);
        if (!propertyExists)
        {
            return NotFound();
        }
        var meters = await _context.Meters.Where(m => m.PropertyId == id).ToListAsync();
        return Ok(meters);
    }
}


