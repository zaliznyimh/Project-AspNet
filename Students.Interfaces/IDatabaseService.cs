using Students.Common.Models;
using System.ComponentModel;

namespace Students.Interfaces;

public interface IDatabaseService
{
    #region IDatabaseService properties for StudentsController
    public bool EditStudent(int id, string name, int age, string major, int[] subjectIdDst);

    Student? DisplayStudentDetails(int? id);
    
    public Task<List<Student>> GetStudentsListAsync();

    public Task<List<Subject>> GetSubjectsList();

    public Task<bool> CreateStudentAsync(int id, string name, int age, string major, int[] subjectIdDst);

    public Task<Student?> GetStudentWithAvailableSubjects(int? id);

    public Task<bool?> DeleteStudentAsync(int? id);

    #endregion // IDatabaseService properties for StudentsController

    #region IDatabaseService properties for SubjectsController 
    public Task<bool> CreateSubjectAsync(Subject subject);

    public Task<Subject?> GetSubjectToEditAsync(int? id);

    public Task<Subject?> EditSubject(Subject subject);

    public Task<Subject?> GetSubjectToDelete (int? id);
    
    public Task<bool> DeleteSubject(int? id);
    
    public bool CheckSubjectExists(int id);
    #endregion // IDatabaseService properties for SubjectsController 
}
