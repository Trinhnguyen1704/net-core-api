using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using net_core_api.Models;
using net_core_api.Models.DTOs;

namespace net_core_api.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DatabaseContext _context;
        public StudentRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Student> AddStudent(StudentDTO student)
        {
            try
            {
                List<Class> classList = _context.Classes.Where(c => student.ClassIds.Contains(c.ClassId)).ToList();
                var newStudent = new Student() {
                    Name = student.Name,
                    DateOfBirth = student.DateOfBirth,
                    AverageMark = student.AverageMark,
                    Classes = classList
                };
                _context.Students.Add(newStudent);
                await _context.SaveChangesAsync();
                return newStudent;
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
                var studentToDelete = await GetStudent(id);
                _context.Students.Remove(studentToDelete);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<Student> GetStudent(int id)
        {
            try
            {
                return await _context.Students
                .Include(x => x.Classes )
                .FirstOrDefaultAsync(student => student.Id == id);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<IEnumerable<Class>> GetClassesByStudentId(int studentId)
        {
            try
            {
                var studentToSelect = await GetStudent(studentId);
                return studentToSelect.Classes;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            try
            {
                return await _context.Students
                .Include(x => x.Classes)
                .AsNoTracking()
                .ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task Update(Student student)
        {
            try
            {
                var studentToUpdate = await GetStudent(student.Id);
                studentToUpdate.Name = student.Name;
                studentToUpdate.DateOfBirth = student.DateOfBirth;
                studentToUpdate.AverageMark = student.AverageMark;
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<IEnumerable<Student>> GetStudentsWithMark(float averageMark)
        {
            try
            {
                return await _context.Students
                .Where(x=> x.AverageMark > averageMark)
                .ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}