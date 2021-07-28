using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using net_core_api.Models;
using net_core_api.Repositories;

namespace net_core_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        [HttpGet("all-students")]
        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _studentRepository.GetStudents();
        }

        [HttpGet("{id}")]
        public async Task<Student> GetStudent(int id)
        {
            return await _studentRepository.GetStudent(id);
        }

        [HttpPost]
        public async Task<Student> AddStudent([FromBody] Student student)
        {
            var newStudent = await _studentRepository.AddStudent(student);
            return newStudent;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudent(int id, [FromBody] Student student)
        {
            if(id != student.Id)
            {
                return BadRequest();
            }
            await _studentRepository.Update(student);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {   
            var studentToDelete = await GetStudent(id);
            if(studentToDelete == null)
            {
                return BadRequest();
            }
            await _studentRepository.Delete(id);
            return NoContent();
        }
    }
}