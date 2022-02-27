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
using Dtos.Student;
using System.Threading;

namespace recapApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StudentsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            var student = await _context
                                         .Students
                                         .Include(x=> x.Classes)
                                         .Include(x=> x.Bus)
                                         .ToListAsync();

            var studentDto = _mapper.Map<List<StudentDto>>(student);

            Thread.Sleep(3000);

            return studentDto;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(int id)
        {
            var student = await _context
                                       .Students
                                       .Include(x => x.Classes)
                                       .Include(x => x.Bus)
                                       .FirstOrDefaultAsync(x => x.Id == id);

            var studentDto = _mapper.Map<StudentDto>(student);

            if (student == null)
            {
                return NotFound();
            }

            return studentDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, StudentDto studentDto)
        {
            if (id != studentDto.Id)
            {
                return BadRequest();
            }

            var student = _mapper.Map<Student>(studentDto);

            var bus = await _context.Buses.FindAsync(studentDto.BusId);
            student.Bus = bus;

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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
        public async Task<ActionResult<Student>> PostStudent(StudentDto studentDto)
            {
            var student = _mapper.Map<Student>(studentDto);

            await UpdateStudentClasses(student, studentDto);

            var bus = await _context.Buses.FindAsync(studentDto.BusId);
            student.Bus = bus;


            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        //private List<int> GetClassesIds(StudentDto studentDto)
        //{
        //    var classesIds = new List<int>();

        //    foreach (var classO in studentDto.Classes)
        //    {
        //        classesIds.Add(classO.Id);
        //    }
        //    return classesIds;
        //}

        private async Task UpdateStudentClasses(Student student, StudentDto studentDto)
        {
            //var classesIds = GetClassesIds(studentDto);

            var classes = await _context
                                        .Classes
                                        .Where(x => studentDto.ClassesId.Contains(x.Id))
                                        .ToListAsync();
            student.Classes.Clear();
            student.Classes.AddRange(classes);
        }

    }

}
