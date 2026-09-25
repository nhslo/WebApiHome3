using WebApiHome3.Models;

namespace WebApiHome3.Services;

public interface IStudentService
{
    IEnumerable<Student> GetAll();
    Student? GetById(int id);
}
