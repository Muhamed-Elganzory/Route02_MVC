using System.ComponentModel.DataAnnotations;

namespace Route02.BLL.DTO.Department;

public class AddDepartmentDto
{
    [Required (ErrorMessage = "Name Is Required")]
    public string Name { get; set; } = null!; // Human Resources
    
    [Required (ErrorMessage = "Code Is Required")]
    [Range(1, int.MaxValue, ErrorMessage = "Most Be The Range From 1 To Max Int")]
    public string Code { get; set; } = null!; // HR
    
    public string? Description { get; set; } // Optional description
    
    public DateTime? CreatedOn { get; set; } // Time of creation
}