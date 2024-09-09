using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TaskManagment.Entities;

namespace TaskManagment.AppServices.Projects
{
    public class ProjectAppService: IProjectAppService
    {

        private readonly IMapper _mapper;
        private readonly TasksDbContext _dbContext;

        public ProjectAppService(IMapper mapper, TasksDbContext tasksDbContext)
        {
            _dbContext = tasksDbContext;
            _mapper = mapper;
            
        }



        public async Task<List<Project>> GetAll()
        {
            var projects = await _dbContext.Projects.ToListAsync();


            return projects;  //_mapper.Map<List<ProjectDto>>( projects);
        }
    }
}
