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
            try 
            {
                _context.Classes.Add(classObject);
                await _context.SaveChangesAsync();
                return classObject;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task Delete(int id)
        {
            try
            {
                var classToDelete = await GetClass(id);
                _context.Classes.Remove(classToDelete);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        public async Task<Class> GetClass(int id)
        {
            try
            {
                return await _context.Classes.Include(x => x.ClassStudents)
                .FirstOrDefaultAsync(c => c.ClassId == id);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<IEnumerable<Class>> GetClasses()
        {
            try
            {
                return await _context.Classes.ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;  
        }

        public async Task<IEnumerable<Class>> GetClassesByName(string className)
        {
            try{
                return await _context.Classes.Include(x => x.ClassStudents).
                Where(x => x.ClassName.Contains(className))
                .ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task Update(Class classObject)
        {
            try
            {
                var classToUpdate = await GetClass(classObject.ClassId);
            
                classToUpdate.ClassName = classObject.ClassName;
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            } 
        }

        public async Task<IEnumerable<Class>> GetClassByStudentId(int id)
        {
            try
            {
                return await _context.Classes.Where(x => x.ClassStudents.Where(s => s.Id == id).Count() > 0)
                .Include(x => x.ClassStudents)
                .AsNoTracking().ToArrayAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<IEnumerable<Student>> GetStudentsByClassId(int id)
        {
            try
            {
                List<int> studentIds = _context.ClassStudents.Where(c => c.ClassId == id).Select(c => c.StudentId).ToList();
                return await _context.Students.Where(c => studentIds.Contains(c.Id)).ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}