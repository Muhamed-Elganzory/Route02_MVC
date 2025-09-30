namespace Route02.BLL.DTO.Department;

public class UpdateDepartmentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!; // Human Resources
    
    public string Code { get; set; } = null!; // HR
    
    public string? Description { get; set; } // Optional description
    
    public DateTime? CreatedOn { get; set; } // Time of creation
}