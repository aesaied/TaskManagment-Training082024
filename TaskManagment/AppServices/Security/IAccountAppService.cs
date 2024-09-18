using TaskManagment.Models;

namespace TaskManagment.AppServices.Security
{
    public interface IAccountAppService
    {
        Task<ResultDto> Register(RegisterViewModel input);
    }
}