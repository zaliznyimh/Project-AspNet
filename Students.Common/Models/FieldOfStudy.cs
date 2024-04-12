namespace Students.Common.Models;

public class FieldOfStudy
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double DurationOfStudies { get; set; }
    public int NumberOfStudents { get; set; }

    public ICollection<Subject>? Subjects { get; set; } 

    public FieldOfStudy()
    {

    }

}
