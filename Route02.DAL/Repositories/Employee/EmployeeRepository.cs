using Route02.DAL.DB.Context;
using Route02.DAL.Repositories.Shared;

namespace Route02.DAL.Repositories.Employee;

public class EmployeeRepository (ApplicationDbContext dbContext): GenericRepository<Models.Employee.Employee>(dbContext), IEmployeeRepository
{
    /*public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }*/
}
