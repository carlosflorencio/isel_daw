using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using API.Services;
using API.TransferModels.InputModels;

namespace API.Data.Contracts
{
    public interface IClassRepository : IGenericRepository<Class>
    {
        Task<Class> GetByIdAsync(int Id);
        Task<PagedList<Class>> GetAllPaginatedAsync(ListQueryStringDto p);
        //
        // ─── Students of Class ────────────────────────────────────────────────────────
        //
        Task<PagedList<Student>> GetClassStudents(int id, ListQueryStringDto query);
        Task<bool> AddStudentTo(Class c, int studentNumberId);
        Task<bool> RemoveStudentFrom(Class c, int studentNumberId);
        //
        // ─── Teachers of Class ────────────────────────────────────────────────────────
        //
        Task<PagedList<Teacher>> GetClassTeachers(int id, ListQueryStringDto query);
        Task<bool> AddTeacherTo(Class c, int teacherNumber);
        Task<bool> RemoveTeacherFrom(Class c, int number);
        //
        // ─── Groups of Class ────────────────────────────────────────────────────────
        //
        Task<PagedList<Group>> GetClassGroups(int id, ListQueryStringDto p);
        // Task<bool> AddGroupTo(Class c, Group group);
        // Task<bool> RemoveGroupFrom(Class c, int groupId);
        
    }
}