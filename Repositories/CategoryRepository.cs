using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using net_core_api.Models;

namespace net_core_api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookContext _context;

        public CategoryRepository(BookContext context)
        {
            _context = context;
        }
        public async Task<Category> AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task DeleteCategory(int id)
        {
            var categoryToDetele = await GetCategory(id);
            _context.Categories.Remove(categoryToDetele);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(category => category.CategoryId == id );
        }

        public async Task UpdateCategory(Category category)
        {
            var categoryToUpdate = await GetCategory(category.CategoryId);

            categoryToUpdate.CategoryName = category.CategoryName;
            await _context.SaveChangesAsync();
        }
    }
}