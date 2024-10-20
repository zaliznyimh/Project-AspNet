﻿using Microsoft.EntityFrameworkCore;
using Students.Common.Models;

namespace Students.Common.Data;

public class StudentsContext : DbContext
{
    public StudentsContext (DbContextOptions<StudentsContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Student { get; set; } = default!;
    public DbSet<Subject> Subject { get; set; } = default!;
    public DbSet<StudentSubject> StudentSubject { get; set; } = default!;
    public DbSet<Lecturer> Lecturers { get; set; } = default!;
    public DbSet<Book> Book { get; set; } = default!;
    public DbSet<FieldOfStudy> FieldOfStudies { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentSubject>()
            .HasKey(ss => new { ss.StudentId, ss.SubjectId });

        modelBuilder.Entity<StudentSubject>()
            .HasOne(ss => ss.Student)
            .WithMany(s => s.StudentSubjects)
            .HasForeignKey(ss => ss.StudentId);

        modelBuilder.Entity<StudentSubject>()
            .HasOne(ss => ss.Subject)
            .WithMany(s => s.StudentSubjects)
            .HasForeignKey(ss => ss.SubjectId);

        modelBuilder.Entity<FieldOfStudy>()
            .HasMany(fos=> fos.Subjects)
            .WithOne(ss=>ss.FieldOfStudy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
