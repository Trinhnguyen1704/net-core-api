using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net_core_api.Models;
using net_core_api.Models.DTOs;

namespace net_core_api.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudent(int id);
        Task<Student> AddStudent(StudentDTO student);
        Task Update(StudentDTO student);
        Task Delete(int id);
        Task<IEnumerable<Class>> GetClassesByStudentId(int studentId);
        Task<IEnumerable<Student>> GetStudentsWithMark(float averageMarkMin, float averageMarkMax);
        Task<IEnumerable<Student>> GetStudentsByName(string studentName);
        Task<IEnumerable<Student>> SortStudentByMark(int sortNo);

        Task<IEnumerable<Student>> GetStudentsInSameClass(int studentId, int classId);

    }
}