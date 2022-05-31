using MediatR;
using AVEVA.PA.MicroserviceTemplate.Application.Dtos;

namespace AVEVA.PA.MicroserviceTemplate.Application.Commands
{
    public class UpdateProjectCommand : IRequest<ReadProjectDto>
    {
        public int Id { get; set; }
        public UpdateProjectDto Project { get; set; }

        public UpdateProjectCommand(int id, UpdateProjectDto project)
        {
            this.Id = id;
            this.Project = project;
        }
    }
}
