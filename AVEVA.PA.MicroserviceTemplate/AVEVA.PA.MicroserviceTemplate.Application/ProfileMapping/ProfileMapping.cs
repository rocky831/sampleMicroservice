using AVEVA.PA.DataAccess.Models;
using AVEVA.PA.MicroserviceTemplate.Application.Dtos;

namespace AVEVA.PA.MicroserviceTemplate.Application.ProfileMapping
{
    public class ProfileMapping : AutoMapper.Profile
    {
        public ProfileMapping()
        {
            this.CreateMap<Project, ReadProjectDto>();
            this.CreateMap<CreateProjectDto, Project>();
            this.CreateMap<UpdateProjectDto, Project>();
        }
    }
}
