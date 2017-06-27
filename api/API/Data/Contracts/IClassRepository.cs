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

        Task<PagedList<Group>> GetClassGroups(int id, ListQueryStringDto p);
        
        Task<bool> AddParticipantTo(Class c, int studentNumberId);
        Task<bool> AddTeacherTo(Class c, int teacherNumber);

        Task<bool> AddGroupTo(Class c);

        Task<List<Student>> GetClassParticipants(int id);

        Task<PagedList<Teacher>> GetClassTeachers(int id, ListQueryStringDto query);
        Task<PagedList<Student>> GetClassStudents(int id, ListQueryStringDto query);
    }
}