using System.ComponentModel.DataAnnotations;
using Route02.DAL.Models.Employee.Enum;

namespace Route02.BLL.DTO.Employee;

public class GetAllEmployeeDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public int Age { get; set; }
    
    public string? Address { get; set; } = null!;
    
    [Display (Name = "Is-Active")]
    public bool IsActive { get; set; }
    
    [DataType(DataType.Currency)]
    public decimal Salary { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; } = null!;
    
    // [Display (Name = "Employee Gender")]
    [Display (Name = "Gender")]
    public string Gender { get; set; } = null!;

    // [Display (Name = "Employee Type")]
    [Display (Name = "Type")]
    public string EmployeeType { get; set; } = null!;
}