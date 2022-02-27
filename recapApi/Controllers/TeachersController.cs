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
using Dtos.Teacher;
using System.Threading;

namespace recapApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TeachersController(
            ApplicationDbContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]           
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetTeachers()
        {
            var teacher = await _context.Teachers.ToListAsync();

            var teacherDto = _mapper.Map<List<TeacherDto>>(teacher);

            return teacherDto;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDto>> GetTeacher(int id)
        {
            var teacher = await _context
                                        .Teachers
                                        .Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

            if (teacher == null)
            {
                return NotFound();
            }

            var teacherDto = _mapper.Map<TeacherDto>(teacher);

            return teacherDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, TeacherDto teacherDto)
        {
            if (id != teacherDto.Id)
            {
                return BadRequest();
            }

            var teacher = _mapper.Map<Teacher>(teacherDto);

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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
        public async Task<ActionResult<Teacher>> PostTeacher(TeacherDto teacherDto)
        {
            var teacher = _mapper.Map<Teacher>(teacherDto);

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeacher", new { id = teacher.Id }, teacher);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }
    }
}
