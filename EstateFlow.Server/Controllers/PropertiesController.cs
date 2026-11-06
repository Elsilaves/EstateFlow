using Microsoft.AspNetCore.Mvc;
using EstateFlow.Server.Data;
using EstateFlow.Server.Domain;
using Microsoft.EntityFrameworkCore;
using EstateFlow.Server.Contracts.Properties;
using EstateFlow.Server.Contracts.Meters;
using Mapster;

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
    public async Task<ActionResult<IEnumerable<PropertyResponse>>> GetProperties()
    {
        var properties = await _context.Properties.ProjectToType<PropertyResponse>().ToListAsync();
        return Ok(properties);
    }

    // GET: api/Properties/{id}
    [HttpGet("{id}")] 
    public async Task<ActionResult<PropertyResponse>> GetProperty(Guid id)
    {
        var property = await _context.Properties.ProjectToType<PropertyResponse>().FirstOrDefaultAsync(p=>p.Id == id);
        if (property is null)
        {
            return NotFound();
        }
        return Ok(property);
    }

    // GET: api/Properties/{id}/meters
    [HttpGet("{id}/meters")]
    public async Task<ActionResult<IEnumerable<MeterResponse>>> GetPropertyMeters(Guid id)
    {
        var propertyExists = await _context.Properties.AnyAsync(p => p.Id == id);
        if (!propertyExists)
        {
            return NotFound();
        }
        var meters = await _context.Meters.Where(m => m.PropertyId == id).ProjectToType<MeterResponse>().ToListAsync();
        return Ok(meters);
    }

    // POST: api/Properties
    [HttpPost]
    public async Task<ActionResult<PropertyResponse>> CreateProperty(CreatePropertyRequest request)
    {
        var property = new Property
        {
            Id = Guid.NewGuid(),
            Code = request.Code,
            Name = request.Name,
            Address = request.Address,
            SquareMeters = request.SquareMeters,
            AdvertisementText = request.AdvertisementText,
            CityId = request.CityId
        };
        _context.Properties.Add(property);
        await _context.SaveChangesAsync();

        var response = await _context.Properties.Where(p=>p.Id == property.Id).ProjectToType<PropertyResponse>().SingleAsync();

        return CreatedAtAction(nameof(GetProperty), new { id = property.Id }, response);
    }

    //PUT: api/Properties/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProperty(Guid id, UpdatePropertyRequest request)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property is null)
        {
            return NotFound();
        }
        property.Name = request.Name;
        property.Address = request.Address;
        property.SquareMeters = request.SquareMeters;
        property.AdvertisementText = request.AdvertisementText;
        property.CityId = request.CityId;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Properties/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProperty(Guid id)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property is null)
        {
            return NotFound();
        }
        _context.Properties.Remove(property);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}


