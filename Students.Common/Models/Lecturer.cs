using Microsoft.EntityFrameworkCore;
using Students.Common.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Students.Common.Models;

[Table("Lecturers")]
public class Lecturer
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string AcademicDegree { get; set; } = string.Empty;
    
    public bool IsPromoter { get; set; }
    
    public List<Subject> Subjects { get; set; } = new List<Subject>();
    
    public Lecturer() {
    
    }

}
