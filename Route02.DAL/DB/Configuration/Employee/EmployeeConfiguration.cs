using Route02.DAL.DB.Configuration.Shared;
using Route02.DAL.Models.Employee.Enum;

namespace Route02.DAL.DB.Configuration.Employee;

public class EmployeeConfiguration: BaseEntityConfiguration<Models.Employee.Employee>, IEntityTypeConfiguration<Models.Employee.Employee>
{
    [Obsolete("Obsolete")]
    public new void Configure(EntityTypeBuilder<Models.Employee.Employee> builder)
    {
        builder.ToTable("Employees");
        builder.HasKey(emp => emp.Id);
        builder.Property(emp => emp.Id).UseIdentityColumn();

        builder
            .HasOne(emp => emp.EmployeeWorkDepartment)
            .WithMany(department => department.Employees)
            .HasForeignKey(emp => emp.FkDepartmentId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.Property(emp => emp.Name).IsRequired().HasColumnType("nvarchar(50)").HasMaxLength(50);
        
        builder.Property(emp => emp.Age).HasColumnType("int");
        builder.HasCheckConstraint("CK_Employee_Age", "Age >= 24 AND Age <= 50");
        
        builder.Property(emp => emp.Address).HasColumnType("nvarchar(100)");
        builder.Property(emp => emp.Email).HasColumnType("nvarchar(100)");
        builder.Property(emp => emp.PhoneNumber).HasColumnType("nvarchar(100)");
        builder.Property(emp => emp.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(emp => emp.Salary).HasColumnType("decimal(10, 2)");
        builder.Property(emp => emp.HiringDate).HasColumnType("date");
        
        // Correct
        builder.Property(emp => emp.Gender)
            .HasConversion((gender) => gender.ToString(),
                (returnGender) => (Gender)Enum.Parse( typeof(Gender), returnGender ));

        // Correct
        builder.Property(emp => emp.EmployeeType)
            .IsRequired()
            .HasConversion<string>();
        
        // Base Entity
        base.Configure(builder);
    }
}