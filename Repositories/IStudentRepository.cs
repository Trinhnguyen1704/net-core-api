using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net_core_api.Models;

namespace net_core_api.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudent(int id);
        Task<Student> AddStudent(Student student);
        Task Update(Student student);
        Task Delete(int id);
        Task<IEnumerable<Class>> GetClassesByStudentId(int studentId);
        Task<IEnumerable<Student>> GetStudentsWithMark(float averageMark);
    }
}