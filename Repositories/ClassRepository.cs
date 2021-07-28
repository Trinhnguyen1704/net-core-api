using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using net_core_api.Models;

namespace net_core_api.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly DatabaseContext _context;
        public ClassRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Class> AddClass(Class classObject)
        {
            _context.Classes.Add(classObject);
            await _context.SaveChangesAsync();
            return classObject;
        }

        public async Task Delete(int id)
        {
            var classToDelete = await GetClass(id);
            _context.Classes.Remove(classToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<Class> GetClass(int id)
        {
            return await _context.Classes.Include(x => x.Students)
            .FirstOrDefaultAsync(c => c.ClassId == id);
        }

        public async Task<Class> GetClassByStudentId(int id)
        {
            return await _context.Classes.Include(x => x.Students)
            .Where(x => x.Students.FirstOrDefault().ClassId == id)
            .FirstOrDefaultAsync(c => c.ClassId == id);
        }

        public async Task<IEnumerable<Class>> GetClasses()
        {
            return await _context.Classes.Include(x => x.Students).ToListAsync();
        }

        public async Task<IEnumerable<Class>> GetClassesByName(string className)
        {
            return await _context.Classes.Include(x => x.Students).
            Where(x => x.ClassName.Contains(className))
            .ToListAsync();
        }

        public async Task Update(Class classObject)
        {
            var classToUpdate = await GetClass(classObject.ClassId);
            
            classToUpdate.ClassName = classObject.ClassName;
            await _context.SaveChangesAsync();
        }
    }
}