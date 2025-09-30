using Microsoft.EntityFrameworkCore;
using Route02.BLL.Mapping;
using Route02.BLL.Mapping.Department;
using Route02.BLL.Mapping.Employee;
using Route02.BLL.Services.Department;
using Route02.BLL.Services.Employee;
using Route02.DAL.DB.Context;
using Route02.DAL.Models.Employee;
using Route02.DAL.Repositories.Department;
using Route02.DAL.Repositories.Employee;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add Repository
// Dependency Injection for DepartmentRepository
// When a controller or service requires IDepartmentRepository, an instance of DepartmentRepository will be provided
// with a scoped lifetime (one instance per request)
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// Service To Auto Mapper
builder.Services.AddAutoMapper(employee => employee.AddProfile(new EmployeeMappingProfile()));
builder.Services.AddAutoMapper(employee => employee.AddProfile(new DepartmentMappingProfile()));

#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();