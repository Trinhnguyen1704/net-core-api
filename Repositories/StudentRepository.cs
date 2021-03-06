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
                List<Class> classes = _context.Classes.Where(c => student.ClassIds.Contains(c.ClassId)).ToList();
                var _newStudent = new Student() {
                    Name = student.Name,
                    DateOfBirth = student.DateOfBirth,
                    AverageMark = student.AverageMark,
                    Classes = classes
                };
                _context.Students.Add(_newStudent);
                await _context.SaveChangesAsync();
                return _newStudent;
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
                .Include(x => x.Classes)
                .AsNoTracking()
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

        public async Task Update(StudentDTO student)
        {
            try
            {
                //List<Class> classes = _context.Classes.Where(c => student.ClassIds.Contains(c.ClassId)).ToList();
                var studentToUpdate = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == student.Id);

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

        public async Task<IEnumerable<Student>> GetStudentsWithMark(float averageMarkMin, float averageMarkMax)
        {
            try
            {
                return await _context.Students
                .Where(x=> x.AverageMark >= averageMarkMin && x.AverageMark <= averageMarkMax)
                .OrderBy(x => x.AverageMark)
                .ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<IEnumerable<Student>> GetStudentsByName(string studentName)
        {
            try{
                return await _context.Students.Include(x => x.Classes)
                .Where(x => x.Name.Contains(studentName))
                .ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<IEnumerable<Student>> SortStudentByMark(int sortNo)
        {
            try{
                if(sortNo == 1) {
                    return await _context.Students.Include(x => x.Classes)
                    .OrderBy(s => s.AverageMark)
                    .AsNoTracking()
                    .ToListAsync();
                }else
                {
                    return await _context.Students.Include(x => x.Classes)
                    .OrderByDescending(s => s.AverageMark)
                    .AsNoTracking()
                    .ToListAsync();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<IEnumerable<Student>> GetStudentsInSameClass(int studentId, int classId)
        {
            try{
                var classObject = await _context.Classes.Include(x => x.Students)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClassId == classId);
                return classObject.Students.Where(s => s.Id != studentId);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}