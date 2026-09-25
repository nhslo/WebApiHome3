using WebApiHome3.Models;

namespace WebApiHome3.Services;

public class StudentService : IStudentService
{
    private readonly List<Student> _students =
    [
        new() { Id = 1, Name = "Aruzhan Saparova", Group = "SE-231" },
        new() { Id = 2, Name = "Daniyar Nurgaliyev", Group = "SE-231" },
        new() { Id = 3, Name = "Madina Serik", Group = "SE-232" }
    ];

    public IEnumerable<Student> GetAll() => _students;

    public Student? GetById(int id) => _students.FirstOrDefault(student => student.Id == id);
}
