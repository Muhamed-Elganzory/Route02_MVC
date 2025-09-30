namespace Route02.DAL.Models.Shared;

public class BaseEntity
{
    public int Id { get; set; } // PK
    
    public int CreatedBy { get; set; } // UserId
    
    public DateTime? CreatedOn { get; set; } // Time of creation
    
    public int LastModificationBy { get; set; } // UserId
    
    public DateTime? LastModificationOn { get; set; } // Time of last modification
    
    public bool IsDeleted { get; set; } // Soft delete flag
}