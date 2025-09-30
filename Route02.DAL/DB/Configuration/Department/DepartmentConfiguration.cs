
using Route02.DAL.DB.Configuration.Shared;

namespace Route02.DAL.DB.Configuration.Department;

public class DepartmentConfiguration: BaseEntityConfiguration <Models.Department.Department>, IEntityTypeConfiguration <Models.Department.Department>
{
    public new void Configure(EntityTypeBuilder<Models.Department.Department> builder)
    {
        builder.HasKey(department => department.Id); // PK
        builder.Property(department => department.Id).UseIdentityColumn(); // Auto-increment
        builder.Property(department => department.Name).IsRequired().HasColumnType("nvarchar(20)");
        builder.Property(department => department.Code).IsRequired().HasColumnType("nvarchar(5)");
        builder.Property(department => department.Description).HasColumnType("nvarchar(max)");
        
        // Base Entity
        base.Configure(builder);
    }
}