using Microsoft.EntityFrameworkCore;
using StudentsPortal.Web.Models.Entities;

namespace StudentsPortal.Web.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }

    }
}
