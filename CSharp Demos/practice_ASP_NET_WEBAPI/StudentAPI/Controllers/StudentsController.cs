using Microsoft.AspNetCore.Mvc;
using StudentAPI.Models;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private static List<Student> _students = new List<Student>
        {
            new Student { Id = 1, Name = "John", Email = "john@test.com", Age = 20 },
            new Student { Id = 2, Name = "Jane", Email = "jane@test.com", Age = 22 }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_students);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound($"Student with id {id} not found");
            return Ok(student);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Student student)
        {
            student.Id = _students.Count == 0 ? 1 : _students.Max(s => s.Id) + 1;
            _students.Add(student);
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Student updatedStudent)
        {
            var existing = _students.FirstOrDefault(s => s.Id == id);
            if (existing == null)
                return NotFound($"Student with id {id} not found");

            existing.Name = updatedStudent.Name;
            existing.Email = updatedStudent.Email;
            existing.Age = updatedStudent.Age;
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound($"Student with id {id} not found");

            _students.Remove(student);
            return NoContent();
        }
    }
}