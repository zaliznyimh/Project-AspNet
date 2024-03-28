using Students.Common.Models;

namespace Students.Interfaces;

public interface IDatabaseService
{
    public bool EditStudent(int id, string name, int age, string major, int[] subjectIdDst);

    Student? DisplayStudent(int? id);
    
    public Task<List<Student>> GetStudentsList();

    public List<Subject> GetListOfSubjects();

    public Task<bool> CreateStudentAsync(int id, string name, int age, string major, int[] subjectIdDst);
}
