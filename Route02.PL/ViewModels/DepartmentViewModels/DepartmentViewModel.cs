namespace Route02.PL.ViewModels.DepartmentViewModels;

public class DepartmentViewModel
{
    public string Name { get; set; } = null!; // Human Resources
    
    public string Code { get; set; } = null!; // HR
    
    public string? Description { get; set; } // Optional description
    
    public DateTime? CreatedOn { get; set; } // Time of creation
}