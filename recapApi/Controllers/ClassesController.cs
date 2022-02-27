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
using Dtos.Class;

namespace recapApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClassesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassDto>>> GetClasses()
        {
            var classs = await _context
                                     .Classes
                                     .Include(x => x.Students)
                                     .Include(x => x.Teacher)
                                     .ToListAsync();

            var classDto = _mapper.Map<List<ClassDto>>(classs);

            return classDto;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClassDto>> GetClass(int id)
        {
            var classs = await _context
                                     .Classes
                                     .Include(x => x.Teacher)
                                     .Where(x => x.Id == id)
                                     .FirstOrDefaultAsync();

            var classDto = _mapper.Map<ClassDto>(classs);

            if (classs == null)
            {
                return NotFound();
            }

            return classDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(int id, ClassDto classDto)
        {
            if (id != classDto.Id)
            {
                return BadRequest();
            }

            var classs = _mapper.Map<Class>(classDto);

            _context.Entry(classs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
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
        public async Task<ActionResult<Class>> PostClass(ClassDto classDto)
        {
            var classs = _mapper.Map<Class>(classDto);

            var teacher = await _context.Teachers.FindAsync(classDto.TeacherId);

            classs.Teacher = teacher;


            _context.Classes.Add(classs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClass", new { id = classs.Id }, classs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var @class = await _context.Classes.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }

            _context.Classes.Remove(@class);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.Id == id);
        }
    }
}
