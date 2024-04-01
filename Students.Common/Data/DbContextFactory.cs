using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;


namespace Students.Common.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<StudentsContext>
    {
        public StudentsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StudentsContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StudentsDatabase;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new StudentsContext(optionsBuilder.Options);
        }
    }
}
