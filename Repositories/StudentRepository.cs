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
            return await _context.Students.Include(x => x.ClassNavigation)
            .FirstOrDefaultAsync(student => student.Id == id);
        }

        public async Task<IEnumerable<Student>> GetStudentsByClassId(int classId)
        {
            return await _context.Students
            .Where(x=> x.ClassId == classId)
            .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _context.Students.Include(x => x.ClassNavigation).ToListAsync();
        }

        public async Task Update(Student student)
        {
            var studentToUpdate = await GetStudent(student.Id);

            studentToUpdate.Name = student.Name;
            studentToUpdate.ClassId = student.ClassId;
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