using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entites;
using recapApi.Data;
using AutoMapper;
using Dtos.Bus;

namespace recapApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class BusesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BusesController(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusDto>>> GetBuses()
        {
            var buses = await _context.Buses.ToListAsync();

            var busesDto = _mapper.Map<List<BusDto>>(buses);

            return busesDto;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BusDto>> GetBus(int id)
        {
            var bus = await _context.Buses
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();

            var busDto = _mapper.Map<BusDto>(bus);

            if (bus == null)
            {
                return NotFound();
            }

            return busDto;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutBus(int id, BusDto busDto)
        {
            if (id != busDto.Id)
            {
                return BadRequest();
            }

            var bus = _mapper.Map<Bus>(busDto);

            _context.Entry(bus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Bus>> PostBus(BusDto busDto)
        {
            var bus = _mapper.Map<Bus>(busDto);

            _context.Buses.Add(bus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBus", new { id = bus.Id }, bus);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBus(int id)
        {
            var bus = await _context.Buses.FindAsync(id);
            if (bus == null)
            {
                return NotFound();
            }

            _context.Buses.Remove(bus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BusExists(int id)
        {
            return _context.Buses.Any(e => e.Id == id);
        }
    }
}
