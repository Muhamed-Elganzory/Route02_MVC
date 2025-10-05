using Route02.DAL.Repositories.Department;
using Route02.DAL.Repositories.Employee;

namespace Route02.DAL.Repositories.Interfaces;

public interface IUnitOfWork
{
    public IEmployeeRepository EmployeeRepository { get; }

    public IDepartmentRepository DepartmentRepository { get; }

    int SaveChanges();
}