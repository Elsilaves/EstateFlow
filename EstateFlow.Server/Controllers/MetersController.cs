using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EstateFlow.Server.Data;
using EstateFlow.Server.Domain;

namespace EstateFlow.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetersController : ControllerBase
    {
        private readonly EstateFlowDbContext _context;
        public MetersController(EstateFlowDbContext context)
        {
            _context = context;
        }
        // GET: api/Meters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meter>>> GetMeters()
        {
            var meters = await _context.Meters.Include(m => m.UtilityType).Include(m => m.Provider).ToListAsync();
            return Ok(meters);
        }

        // GET: api/Meters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Meter>> GetMeter(Guid id)
        {
            var meters = await _context.Meters.Include(m => m.UtilityType).Include(m => m.Provider).FirstOrDefaultAsync(m => m.Id == id);
            if (meters == null)
            {
                return NotFound();
            }
            return Ok(meters);
        }

        // Get: /api/Meters/{id}/readings
        [HttpGet("{id}/readings")]
        public async Task<ActionResult<IEnumerable<MeterReading>>> GetMeterReadings(Guid id)
        {
            var meterExists = await _context.Meters.AnyAsync(m => m.Id == id);
            if (!meterExists)
            {
                return NotFound();
            }
            var readings = await _context.MeterReadings.Where(r => r.MeterId == id).OrderBy(r => r.MesuredAt).ToListAsync();
            return Ok(readings);
        }
    }
}
