using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using net_core_api.Models;
using net_core_api.Models.DTOs;
using net_core_api.Repositories;

namespace net_core_api.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _studentRepository.GetStudents();
        }

        [HttpGet("{id}")]
        public async Task<Student> GetStudent(int id)
        {
            return await _studentRepository.GetStudent(id);
        }

        [HttpGet("{studentId}/classes")]
        public async Task<IEnumerable<Class>> GetClassesByStudentId(int studentId)
        {
            return await _studentRepository.GetClassesByStudentId(studentId);
        }

        [HttpPost]
        public async Task<Student> AddStudent([FromBody] StudentDTO student)
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
        
        [HttpGet("mark")]
        public async Task<IEnumerable<Student>> GetStudentsWithMark([FromQuery] float averageMark)
        {
            return await _studentRepository.GetStudentsWithMark(averageMark);
        }

        [HttpGet("name")]
        public async Task<IEnumerable<Student>> GetStudentsByName([FromQuery] string studentName)
        {
            return await _studentRepository.GetStudentsByName(studentName);
        }
    }
}