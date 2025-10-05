using System.Reflection;
using Route02.DAL.Models.Department;
using Route02.DAL.Models.Employee;

namespace Route02.DAL.DB.Context;

public class ApplicationDbContext (DbContextOptions <ApplicationDbContext> options): DbContext (options)
{
    //
    /// The OnConfiguring method is commented out to use the connection string from Program.cs
    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     {
         optionsBuilder.UseSqlServer("Server= localhost,1433; Database= Route02_MVC; User Id=sa;Password=YourStrong@Passw0rd; TrustServerCertificate=True;");
    }*/
    
    // Constructor to initialize the DbContext with options.
    // This allows configuration from outside, such as in Program.cs.
    // The options parameter is passed to the base DbContext class.
    // This is essential for setting up the database connection and other settings.
    // public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    // {
    //     
    // }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
}