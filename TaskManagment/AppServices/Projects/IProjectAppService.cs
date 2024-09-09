using TaskManagment.Entities;

namespace TaskManagment.AppServices.Projects
{
    public interface IProjectAppService
    {
        Task<List<Project>> GetAll();
    }
}