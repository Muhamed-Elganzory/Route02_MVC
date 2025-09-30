using System.ComponentModel.DataAnnotations;
using Route02.DAL.Models.Employee.Enum;

namespace Route02.PL.ViewModels.EmployeeViewModel;

public class EmployeeViewModels
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50, ErrorMessage = "Max length should be 50 character")]
    [MinLength(3, ErrorMessage = "Min length should be 3 characters")]
    public string Name { get; set; } = null!;
    
    [Range(22, 30)]
    public int Age { get; set; }
    
    [RegularExpression("^[1-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}", ErrorMessage = "Address Must Be Like 123-Street-City-Country")]
    public string? Address { get; set; }
    
    [Display (Name = "Is Active")]
    public bool IsActive { get; set; }
    
    [DataType(DataType.Currency)]
    public decimal Salary { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; }
    
    [Phone]
    [Display (Name = "Phone Number")]
    public string? PhoneNumber { get; set; }

    [Display (Name = "Hiring Date")]
    public DateOnly HiringDate { get; set; }
    
    [Display (Name = "Employee Gender")]
    public Gender Gender { get; set; }

    [Display (Name = "Employee Type")]
    public EmployeeType EmployeeType { get; set; }
    
    public int CreatedBy { get; set; } // UserId
    
    public int LastModificationBy { get; set; } // UserId
}