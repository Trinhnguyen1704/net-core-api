using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using net_core_api.Models;

namespace net_core_api.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DatabaseContext _context;
        public StudentRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Student> AddStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task Delete(int id)
        {
            var studentToDelete = await GetStudent(id);
            _context.Students.Remove(studentToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<Student> GetStudent(int id)
        {
            return await _context.Students
            .Include(x => x.Classes )
            .FirstOrDefaultAsync(student => student.Id == id);
        }

        public async Task<IEnumerable<Class>> GetClassesByStudentId(int studentId)
        {
            var studentToSelect = await GetStudent(studentId);
            return studentToSelect.Classes;
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _context.Students
            .Include(x => x.Classes)
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task Update(Student student)
        {
            var studentToUpdate = await GetStudent(student.Id);

            studentToUpdate.Name = student.Name;
            studentToUpdate.DateOfBirth = student.DateOfBirth;
            studentToUpdate.AverageMark = student.AverageMark;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsWithMark(float averageMark)
        {
            return await _context.Students
            .Where(x=> x.AverageMark > averageMark)
            .ToListAsync();
        }
    }
}