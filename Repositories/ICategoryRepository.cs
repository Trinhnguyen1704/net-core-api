using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net_core_api.Models;

namespace net_core_api.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        Task<Category> AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(int id);
    }
}