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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRespository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRespository = categoryRepository;
        }
        [HttpGet("all-categories")]
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _categoryRespository.GetCategories();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            return await _categoryRespository.GetCategory(id);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory([FromBody] Category category)
        {
            var newCategory = await _categoryRespository.AddCategory(category);
            return newCategory;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            if(id != category.CategoryId)
            {
                return BadRequest();
            }
            await _categoryRespository.UpdateCategory(category);

            return NoContent();
        }

        [HttpDelete("{id}")]
         public async Task<ActionResult> DeleteCategory(int id)
        {
            var categoryToDelete = await _categoryRespository.GetCategory(id);
            if(categoryToDelete == null)
            {
                return NotFound();
            }
            
            await _categoryRespository.DeleteCategory(categoryToDelete.CategoryId);
            return NoContent();
        }
    }
}