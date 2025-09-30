using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Route02.DAL.Models.Employee.Enum;
using Route02.DAL.Models.Shared;

namespace Route02.DAL.Models.Employee;

public class Employee: BaseEntity
{
    public string Name { get; set; } = null!;
    
    public int Age { get; set; }
    
    public string? Address { get; set; }
    
    public bool IsActive { get; set; }
    
    public decimal Salary { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; }
    
    public string? PhoneNumber { get; set; }

    public DateOnly HiringDate { get; set; }
    
    public Gender Gender { get; set; }

    public EmployeeType EmployeeType { get; set; }
    
    // Navigation Property
    [InverseProperty(nameof(Department.Department.Employees))]
    public Department.Department? EmployeeWorkDepartment { get; set; }
    
    // Foreign Key
    public int? FkDepartmentId { get; set; }
}