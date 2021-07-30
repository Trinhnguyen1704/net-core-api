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
                var _student = new Student()
                {
                    Name = student.Name,
                    DateOfBirth = student.DateOfBirth,
                    AverageMark = student.AverageMark,
                };
                _context.Students.Add(_student);
                await _context.SaveChangesAsync();
                foreach(var id in student.ClassIds)
                {
                    var _class_student = new ClassStudent()
                    {
                        StudentId = _student.Id,
                        ClassId = id
                    };
                    _context.ClassStudents.Add(_class_student);
                     await _context.SaveChangesAsync();
                }
            
                return _student;
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
                .Include(x => x.ClassStudents)
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
                List<int> classIds = _context.ClassStudents.Where(c => c.StudentId == studentId).Select(c => c.ClassId).ToList();
                return await _context.Classes.Where(c => classIds.Contains(c.ClassId)).ToListAsync();
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
                .Include(x => x.ClassStudents)
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