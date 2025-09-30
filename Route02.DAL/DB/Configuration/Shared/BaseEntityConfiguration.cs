using Route02.DAL.Models.Shared;

namespace Route02.DAL.DB.Configuration.Shared;

public class BaseEntityConfiguration<T>: IEntityTypeConfiguration<T> where T : BaseEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(department => department.CreatedOn).IsRequired().HasDefaultValueSql("GETDATE()"); // Default to current date
        builder.Property(department => department.LastModificationOn).HasComputedColumnSql("GETDATE()"); // Update to current date on modification
    }
}