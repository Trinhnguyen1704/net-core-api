using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net_core_api.Models;
using net_core_api.Models.DTOs;

namespace net_core_api.Repositories
{
    public interface IClassRepository
    {
        Task<IEnumerable<Class>> GetClasses();
        Task<Class> GetClass(int id);
        Task<Class> AddClass(Class classObject);
        Task Update(Class classObject);
        Task Delete(int id);
        Task<IEnumerable<Class>> GetClassesByName(string className);
        Task <IEnumerable<Class>> GetClassByStudentId(int id);
        Task <IEnumerable<Student>> GetStudentsByClassId(int id);

        Task<IEnumerable<ClassSatisticDTO>> GetTotalStudents();
    }
}