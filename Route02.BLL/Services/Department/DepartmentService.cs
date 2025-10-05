using AutoMapper;
using Route02.BLL.DTO;
using Route02.BLL.DTO.Department;
using Route02.BLL.Factories.Department;
using Route02.DAL.Repositories.Department;
using Route02.DAL.Repositories.Interfaces;

namespace Route02.BLL.Services.Department;

// IDepartmentRepository departmentRepository ==> IUnitOfWork unitOfWork
public class DepartmentService(IUnitOfWork unitOfWork, IMapper mapper): IDepartmentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    // Get all departments
    public IEnumerable<GetAllDepartmentsDto> GetAllDepartments()
    {
        var departments = _unitOfWork.DepartmentRepository.GetAll();

        // Manual mapping from Department to DepartmentDto
        // var departmentDto = departments.Select(dept => new AllDepartmentsDto
        // {
        //     DeptId = dept.Id,
        //     Name = dept.Name,
        //     Code = dept.Code,
        //     Description = dept.Description,
        //     CreatedOn = dept.CreatedOn
        // });
        // return departmentDto;
        
        // Using Auto Mapping
        var departmentDto = mapper.Map<IEnumerable<DAL.Models.Department.Department>, IEnumerable<GetAllDepartmentsDto>>(departments);
        return departmentDto;

        // Using factory method for mapping (Extension Method)
        // return departments.Select(deptFactory => deptFactory.ToGetAllDepartmentsDto());
    }

    // Get department by ID
    public GetDepartmentByIdDto? GetDepartmentById(int id)
    {
        
        var department = _unitOfWork.DepartmentRepository.GetById(id);

        // if (department is not null)
        // {
        //     var departmentDetailsDto = new DepartmentDetailsDto
        //     {
        //         ID = department.ID,
        //         Name = department.Name,
        //         Code = department.Code,
        //         Description = department.Description,
        //         CreatedBy = department.CreatedBy,
        //         CreatedOn = department.CreatedOn,
        //         LastModificationBy = department.LastModificationBy,
        //         LastModificationOn = department.LastModificationOn,
        //         IsDeleted = department.IsDeleted
        //     };
        //     
        //     return departmentDetailsDto;
        // }
        //
        // return null;

        // return department is null ? null : department.ToGetDepartmentByIdDto();

        if (department == null) return null;

        // Using Auto Mapping
        var departmentDto = mapper.Map<DAL.Models.Department.Department, GetDepartmentByIdDto>(department);
        return departmentDto;
    }
    
    // Add a new department
    public int AddDepartment(CreateDepartmentDto department)
    {
        // var deptDto = department.ToAddDepartmentDto();
        // return departmentRepository.Add (deptDto);
        
        // Using Auto Mapping
        var departmentDto = mapper.Map<CreateDepartmentDto, DAL.Models.Department.Department>(department);
        _unitOfWork.DepartmentRepository.Add(departmentDto);

        return _unitOfWork.SaveChanges();
    }
    
    // Update an existing department
    public int UpdateDepartment(UpdateDepartmentDto department)
    {
        // return departmentRepository.Update (departmentDto.ToUpdateDepartmentDto());
        
        // Using Auto Mapping
        var departmentDto = mapper.Map<UpdateDepartmentDto, DAL.Models.Department.Department>(department);
        _unitOfWork.DepartmentRepository.Update(departmentDto);

        return _unitOfWork.SaveChanges();
    }
    
    // Delete a department by ID
    public bool DeleteDepartment(int id)
    {
        var department = _unitOfWork.DepartmentRepository.GetById (id);
        
        if(department == null) return false;
        
        department.IsDeleted = true;
        _unitOfWork.DepartmentRepository.Update(department);

        return _unitOfWork.SaveChanges() > 0 ? true : false;

        /*if (department is not null)
        {
            var result = departmentRepository.Delete (department);

            return result > 0 ? true : false;
        }*/

        // return false;
    }
}