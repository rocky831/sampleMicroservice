using MediatR;
using AVEVA.PA.MicroserviceTemplate.Application.Dtos;

namespace AVEVA.PA.MicroserviceTemplate.Application.Commands
{
    public class CreateProjectCommand : IRequest<ProjectAccepted>
    {
        public CreateProjectDto? Project { get; set; }
    }
}
