using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using net_core_api.Models;
using net_core_api.Repositories;

namespace net_core_api.Controllers
{
    [Route("api/classes")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassRepository _classRepository;
        public ClassController(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<Class>> GetClasses()
        {
            return await _classRepository.GetClasses();
        }

        [HttpGet("{id}")]
        public async Task<Class> GetClass(int id)
        {
            return await _classRepository.GetClass(id);
        }

        [HttpPost]
        public async Task<Class> AddClass([FromBody] Class classObject)
        {
            var newClass = await _classRepository.AddClass(classObject);
            return newClass;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClass(int id, [FromBody] Class classObject)
        {
            if(id != classObject.ClassId)
            {
                return BadRequest();
            }
            await _classRepository.Update(classObject);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var classToDelete = await GetClass(id);
            if(classToDelete == null)
            {
                return BadRequest();
            }
            await _classRepository.Delete(id);
            return NoContent();
        }

        [HttpGet("class-name")]
        public async Task<IEnumerable<Class>> GetClassesByName([FromQuery] string className )
        {
            return await _classRepository.GetClassesByName(className);
        }

        [HttpGet("student/{studentId}")]
        public async Task<IEnumerable<Class>> GetClassByStudentId(int studentId)
        {
            return await _classRepository.GetClassByStudentId(studentId);
        }
    }
}