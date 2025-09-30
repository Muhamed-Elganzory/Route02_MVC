using System.ComponentModel.DataAnnotations;
using Route02.DAL.Models.Employee.Enum;

namespace Route02.BLL.DTO.Employee;

public class GetEmployeeDetailsDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public int Age { get; set; }
    
    
    public string? Address { get; set; }
    
    [Display (Name = "Is-Active")]
    public bool IsActive { get; set; }
    
    [DataType(DataType.Currency)]
    public decimal Salary { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; }
    
    [Phone]
    public string? PhoneNumber { get; set; }

    public DateOnly HiringDate { get; set; }
    
    public int CreatedBy { get; set; } // UserId
    
    public DateTime? CreatedOn { get; set; } // Time of creation
    
    public int LastModificationBy { get; set; } // UserId
    
    [Display (Name = "Employee Gender")]
    public string EmpGender { get; set; } = null!;
    
    public DateTime? LastModificationOn { get; set; } // Time of last modification

    [Display (Name = "Employee Type")]
    public string? EmployeeType { get; set; } = null!;
}