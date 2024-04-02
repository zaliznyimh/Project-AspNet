using Microsoft.EntityFrameworkCore;
using Students.Common.Models;
using System.ComponentModel;

namespace Students.Interfaces;

public interface IDatabaseService
{
    #region IDatabaseService properties for StudentsController
    
    public bool EditStudent(int id, string name, int age, string major, int[] subjectIdDst);

    public Task<Student> EditStudentAsync(Student student, int[] subjectIdDst);
    Student? DisplayStudentDetails(int? id);
    
    public Task<List<Student>> GetStudentsListAsync();

    public Task<Student> CreateStudentAsync(Student student, int[] subjectIdDst);
    public Task<Student?> GetStudentWithAvailableSubjects(int? id);

    public Task<bool?> DeleteStudentAsync(int? id);

    #endregion // IDatabaseService properties for StudentsController

    #region IDatabaseService properties for SubjectsController 
    public Task<List<Subject>> GetSubjectsList();
    
    public Task<bool> CreateSubjectAsync(Subject subject);

    public Task<Subject?> GetSubjectToEditAsync(int? id);

    public Task<Subject?> EditSubject(Subject subject);

    public Task<Subject?> GetSubjectToDelete (int? id);
    
    public Task<bool> DeleteSubject(int? id);
    
    public bool CheckSubjectExists(int id);
    #endregion // IDatabaseService properties for SubjectsController 

    #region IDatabaseService properties for LecturerController 

    public Task<List<Lecturer>> GetLecturersList();

    public Task<Lecturer?> GetLecturerInfo(int? id);
    public Task<bool> CreateLecturerAsync(Lecturer lecturer);
    public Task<Lecturer?> EditLecturer(Lecturer lecturer);

    public Task<bool> DeleteLecturer(int? id);

    #endregion // IDatabaseService properties for LecturerController 

    #region IDatabaseService properties for BooksController 

    public Task<List<Book>> GetBooksList();
    public Task<Book?> GetInfoBook(int? id);
    public Task<bool> CreateBookAsync(Book book);
    public Task<Book?> EditBook(Book book);
    public Task<bool> DeleteBook(int? id);
    #endregion // IDatabaseService properties for BooksController 

    
}
