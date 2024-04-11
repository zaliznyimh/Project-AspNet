using Microsoft.Extensions.Logging;
using Students.Common.Data;
using Students.Common.Models;
using Students.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Students.Services;

public class DatabaseService : IDatabaseService
{
    #region Ctor and Properties

    private readonly StudentsContext _context;
    private readonly ILogger<DatabaseService> _logger;

    public DatabaseService(
        ILogger<DatabaseService> logger,
        StudentsContext context)
    {
        _logger = logger;
        _context = context;
    }

    #endregion // Ctor and Properties

    #region Public Methods

    #region StudentsController Methods
    public bool EditStudent(int id, string name, int age, string major, int[] subjectIdDst)
    {
        var result = false;

        // Find the student
        var student = _context.Student.Find(id);
        if (student != null)
        {
            // Update the student's properties
            student.Name = name;
            student.Age = age;
            student.Major = major;

            // Get the chosen subjects
            var chosenSubjects = _context.Subject
                .Where(s => subjectIdDst.Contains(s.Id))
                .ToList();

            // Remove the existing StudentSubject entities for the student
            var studentSubjects = _context.StudentSubject
                .Where(ss => ss.StudentId == id)
                .ToList();
            _context.StudentSubject.RemoveRange(studentSubjects);

            // Add new StudentSubject entities for the chosen subjects
            foreach (var subject in chosenSubjects)
            {
                var studentSubject = new StudentSubject
                {
                    Student = student,
                    Subject = subject
                };
                _context.StudentSubject.Add(studentSubject);
            }

            // Save changes to the database
            var resultInt = _context.SaveChanges();
            result = resultInt > 0;
        }

        return result;
    }
    public async Task<Student> EditStudentAsync(Student student, int[] subjectIdDst)
    {
        try
        {
            var chosenSubjects = _context.Subject
                .Where(s => subjectIdDst.Contains(s.Id))
                .ToList();
            var availableSubjects = _context.Subject
                .Where(s => !subjectIdDst.Contains(s.Id))
                .ToList();

            student.AvailableSubjects = availableSubjects;

            foreach (var chosenSubject in chosenSubjects)
            {
                student.AddSubject(chosenSubject);
            }

            _context.Add(student);
            var addResult = await _context.SaveChangesAsync();
            {
                throw new Exception("An error occurred during saving data");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }
        return student;
    }

    public Student? GetStudentInfoAsync(int? id)
    {
        Student? student = null;
        try
        {
            student = _context.Student.FirstOrDefault(m => m.Id == id);
            if (student is not null)
            {
                 var studentSubjects = _context.StudentSubject
                    .Where(ss => ss.StudentId == id)
                    .Include(ss => ss.Subject)
                    .ToList();
                student.StudentSubjects = studentSubjects;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught in DisplayStudent: " + ex);
        }

        return student;
    }

    public async Task<List<Student>> GetStudentsListAsync()
    {
        var studentList = new List<Student>();
        try
        {
            studentList = await _context.Student.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught in GetStudentList: " + ex);
        }
        return studentList;
    }

    public async Task<List<Subject>> GetSubjectsList()
    {
        var listOfSubjects = new List<Subject>();
        try
        {
            listOfSubjects = await _context.Subject.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught in GetListOfSubjects: " + ex);
        }
        return listOfSubjects;
    }

    public async Task<Student> CreateStudentAsync(Student student, int[] subjectIdDst)
    {
        try
        {
            var chosenSubjects = _context.Subject
                .Where(s => subjectIdDst.Contains(s.Id))
                .ToList();
            var availableSubjects = _context.Subject
                .Where(s => !subjectIdDst.Contains(s.Id))
                .ToList();

            student.AvailableSubjects = availableSubjects;

            foreach (var chosenSubject in chosenSubjects)
            {
                student.AddSubject(chosenSubject);
            }

            await _context.Student.AddAsync(student);
            var addResult = await _context.SaveChangesAsync();
            {
                throw new Exception("An error occurred during saving data");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }
        return student;
    }

    public async Task<Student?> GetStudentWithAvailableSubjects(int? id)
    {
        Student? student = await _context.Student.FindAsync(id);
        try
        {
                if (student is not null)
                {
                    var chosenSubjects = _context.StudentSubject
                        .Where(ss => ss.StudentId == id)
                        .Select(ss => ss.Subject)
                        .ToList();
                    var availableSubjects = _context.Subject
                        .Where(s => !chosenSubjects.Contains(s))
                        .ToList();
                    student.StudentSubjects = _context.StudentSubject
                        .Where(x => x.StudentId == id)
                        .ToList();
                    student.AvailableSubjects = availableSubjects;
                }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught in EditStudent: " + ex.Message);
        }
        return student;
    }

    public async Task<bool?> DeleteStudentAsync(int? id)
    {
        var result = false;
        try
        {
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }

            await _context.SaveChangesAsync();
            var saveResult = await _context.SaveChangesAsync();
            result = saveResult > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }
        return result;
    }
    #endregion // StudentsController Methods

    #region SubjectsController Methods
    public async Task<bool> CreateSubjectAsync(Subject subject)
    {
        var result = false; 
        await _context.AddAsync(subject);
        var saveResult = await _context.SaveChangesAsync();
        result = saveResult > 0;
        return result;
    } 

    public async Task<Subject?> GetSubjectToEditAsync(int? id)
    {
        var subject = await _context.Subject.Include(x=>x.FieldOfStudy).SingleOrDefaultAsync(x=>x.Id == id);
        return subject;
    }

    public async Task<bool?> EditSubject(Subject subject)
    {
        _context.Update(subject);
        var saveResult = await _context.SaveChangesAsync();
        var result = saveResult > 0;
        return result;
    }

    public async Task<Subject?> GetSubjectToDelete(int? id)
    {
        var subject = await _context.Subject.
                            FirstOrDefaultAsync(m => m.Id == id);
        return subject;
    }

    public async Task<bool> DeleteSubject(int? id)
    {
        var result = false;
        var subject = await _context.Subject.FindAsync(id);
        if (subject != null)
        {
            _context.Subject.Remove(subject);
        }

        var saveResult = await _context.SaveChangesAsync();
        result = saveResult > 0;
        return result;
    }

    public bool CheckSubjectExists(int id)
    {
        var result = _context.Subject.Any(e => e.Id == id);
        return result;
    }
    #endregion // LecturersController Methods

    #region LecturersController Methods

    public async Task<List<Lecturer>> GetLecturersList()
    {
        var listOfLecturers = new List<Lecturer>();
        try
        {
            listOfLecturers = await _context.Lecturers.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught in GetListOfSubjects: " + ex);
        }
        return listOfLecturers;

    }

    public async Task<Lecturer?> GetLecturerInfo(int? id)
    {
        var lecturer = await _context.Lecturers.FirstOrDefaultAsync(m => m.Id == id);
        return lecturer;
    }

    public async Task<bool> CreateLecturerAsync(Lecturer lecturer)
    {
        var result = false;
        await _context.AddAsync(lecturer);
        var saveResult = await _context.SaveChangesAsync();
        result = saveResult > 0;
        return result;
    }

    public async Task<Lecturer?> EditLecturer(Lecturer lecturer)
    {
        _context.Update(lecturer);
        var saveResult = await _context.SaveChangesAsync();
        var result = saveResult > 0;
        return lecturer;
    }
    public async Task<bool> DeleteLecturer(int? id)
    {
        var result = false;
        var lecturer = await _context.Lecturers.FindAsync(id);
        if (lecturer != null)
        {
            _context.Lecturers.Remove(lecturer);
        }

        var saveResult = await _context.SaveChangesAsync();
        result = saveResult > 0;
        return result;

    }
    #endregion // LecturersController Methods

    #region BooksController Methods
    public async Task<List<Book>> GetBooksList()
    {
        var listOfBooks = new List<Book>();
        try
        {
            listOfBooks = await _context.Book.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught in GetListOfSubjects: " + ex);
        }
        return listOfBooks;

    }
    public async Task<Book?> GetInfoBook(int? id)
    {
        var bookInfo = await _context.Book.FirstOrDefaultAsync(m => m.Id == id);
        return bookInfo;
    }
    public async Task<bool> CreateBookAsync(Book book)
    {
        var result = false;
        await _context.AddAsync(book);
        var saveResult = await _context.SaveChangesAsync();
        result = saveResult > 0;
        return result;
    }

    public async Task<Book?> EditBook(Book book)
    {
        _context.Update(book);
        var saveResult = await _context.SaveChangesAsync();
        var result = saveResult > 0;
        return book;
    }
    public async Task<bool> DeleteBook(int? id)
    {
        var result = false;
        var bookToDelete = await _context.Book.FindAsync(id);
        if (bookToDelete != null)
        {
            _context.Book.Remove(bookToDelete);
        }

        var saveResult = await _context.SaveChangesAsync();
        result = saveResult > 0;
        return result;
    }

    #endregion // LecturersController Methods

    #region FieldOfStudyController Methods

    public async Task<List<FieldOfStudy>> GetFieldOfStudyListAsync()
    {
        var listOfFields = new List<FieldOfStudy>();
        listOfFields = await _context.FieldOfStudies.ToListAsync();
        return listOfFields;
    }

    public async Task<FieldOfStudy?> GetFieldOfStudyInfoAsync(int? id) {
        var fieldOfStudy = await _context.FieldOfStudies
                                .FirstOrDefaultAsync(m => m.Id == id);
        return fieldOfStudy;
    }

    public async Task<bool> CreateFieldOfStudyAsync(FieldOfStudy fieldOfStudy)
    {
        var result = false;
        await _context.AddAsync(fieldOfStudy);
        var saveResult = await _context.SaveChangesAsync();
        result = saveResult > 0;
        return result;
    }

    public async Task<FieldOfStudy?> EditFieldOfStudyAsync(FieldOfStudy fieldOfStudy)
    {
        _context.Update(fieldOfStudy);
        var saveResult = await _context.SaveChangesAsync();
        var result = saveResult > 0;
        return fieldOfStudy;
    }

    public async Task<bool> DeleteFieldOfStudyAsync(int? id)
    {
        var result = false;
        var fieldOfStudy = await _context.FieldOfStudies.Include(x=> x.Subjects).SingleOrDefaultAsync(x => x.Id == id);
        if (fieldOfStudy != null)
        {
            _context.FieldOfStudies.Remove(fieldOfStudy);
        }

        var saveResult = await _context.SaveChangesAsync();
        result = saveResult > 0;
        return result;
    }

    #endregion //FieldOfStudyController Methods

    #endregion // Public Methods
}
