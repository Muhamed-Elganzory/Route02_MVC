using Route02.DAL.DB.Context;
using Route02.DAL.Repositories.Shared;

namespace Route02.DAL.Repositories.Department;

public class DepartmentRepository(ApplicationDbContext dbContext): GenericRepository<Models.Department.Department>(dbContext), IDepartmentRepository
{
    
}