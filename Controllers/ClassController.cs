using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using net_core_api.Models;
using net_core_api.Models.DTOs;
using net_core_api.Repositories;

namespace net_core_api.Controllers
{
    [Route("api/classes")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClassRepository _classRepository;
        public ClassController(IClassRepository classRepository, IMapper mapper)
        {
            _classRepository = classRepository;
            _mapper = mapper;
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

        [HttpGet("{classId}/students")]
        public async Task<IEnumerable<Student>> GetStudentsByClassId(int classId)
        {
            return await _classRepository.GetStudentsByClassId(classId);
        }

        [HttpPost]
        public async Task<Class> AddClass([FromBody] ClassDTO classObject)
        {
            return await _classRepository.AddClass(_mapper.Map<Class>(classObject));
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

    }
}