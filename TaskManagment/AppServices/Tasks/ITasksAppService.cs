using TaskManagment.Models;

namespace TaskManagment.AppServices.Tasks
{
    public interface ITasksAppService
    {
        Task<bool> Create(CreateTaskModel input);
        Task<PageResult<TaskModel>> GetAll(DataTableFilter filter);
    }
}