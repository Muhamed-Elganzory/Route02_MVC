namespace Route02.BLL.DTO.Department;

public class GetAllDepartmentsDto
{
    // From Department
    public string Name { get; set; } = null!; // Human Resources
    
    public string Code { get; set; } = null!; // HR
    
    public string? Description { get; set; } // Optional description
    
    // From BaseEntity
    public int DeptId { get; set; }
    
    public DateTime? CreatedOn { get; set; } // Time of creation
}