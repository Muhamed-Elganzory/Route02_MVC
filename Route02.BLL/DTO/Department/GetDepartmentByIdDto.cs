namespace Route02.BLL.DTO.Department;

public class GetDepartmentByIdDto
{
    public int Id { get; set; } // PK
    public string Name { get; set; } = null!; // Human Resources
    
    public string Code { get; set; } = null!; // HR
    
    public string? Description { get; set; } // Optional description
    
    public int CreatedBy { get; set; } // UserId
    
    public DateTime? CreatedOn { get; set; } // Time of creation
    
    public int LastModificationBy { get; set; } // UserId
    
    public DateTime? LastModificationOn { get; set; } // Time of last modification
    
    public bool IsDeleted { get; set; } // Soft delete flag
}