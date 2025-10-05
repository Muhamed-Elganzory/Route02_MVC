using Route02.DAL.DB.Context;
using Route02.DAL.Repositories.Department;
using Route02.DAL.Repositories.Employee;
using Route02.DAL.Repositories.Interfaces;

namespace Route02.DAL.Repositories.Unit;

public class UnitOfWork: IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private readonly Lazy<IEmployeeRepository> _employeeRepository;
    private readonly Lazy<IDepartmentRepository> _departmentRepository;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(_dbContext));
        _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(_dbContext));
    }

    public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

    public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;

    public int SaveChanges()
    {
        return _dbContext.SaveChanges();
    }
}