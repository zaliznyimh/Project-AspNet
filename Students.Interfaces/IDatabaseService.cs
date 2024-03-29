using Students.Common.Models;

namespace Students.Interfaces;

public interface IDatabaseService
{
    public bool EditStudent(int id, string name, int age, string major, int[] subjectIdDst);

    Student? DisplayStudentDetails(int? id);
    
    public Task<List<Student>> GetStudentsListAsync();

    public Task<List<Subject>> GetListOfSubjects();

    public Task<bool> CreateStudentAsync(int id, string name, int age, string major, int[] subjectIdDst);

    public Task<Student?> GetStudentWithAvailableSubjects(int? id);

    public Task<bool?> DeleteStudentAsync(int? id);

    public Task<bool> CreateSubject(Subject subject);

}
