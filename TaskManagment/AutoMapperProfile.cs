using AutoMapper;
using TaskManagment.Models;

namespace TaskManagment
{
    public class AutoMapperProfile:Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<ETask, TaskModel>()
                .ForMember(d => d.ProjectName, s => s.MapFrom(ss => ss.Project.Name))
                .ForMember(d => d.HasAttachment, s => s.MapFrom(ss => ss.Attachment != null));


        }
    }
}
