using TaskManagment.Models;

namespace TaskManagment.AppServices.Employees
{
    public interface IEmployeeAppService
    {
        Task<bool> Create(CreateEmployeeDto input);
        Task<PageResult<EmployeeDto>> GetAll(DataTableFilter filter);
        Task<CreateEmployeeDto?> GetForEditById(int id);
        Task<bool> Update(CreateEmployeeDto input);
    }
}