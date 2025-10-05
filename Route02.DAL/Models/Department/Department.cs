using System.ComponentModel.DataAnnotations.Schema;
using Route02.DAL.Models.Shared;

namespace Route02.DAL.Models.Department;

public class Department: BaseEntity
{
    public string Name { get; set; } = null!; // Human Resources
    
    public string Code { get; set; } = null!; // HR

    public string? Description { get; set; } // Optional description

    // Navigation Property
    [InverseProperty(nameof(Employee.Employee.EmployeeWorkDepartment))]
    public virtual ICollection <Employee.Employee> Employees { get; set; } = new HashSet<Employee.Employee>();
}