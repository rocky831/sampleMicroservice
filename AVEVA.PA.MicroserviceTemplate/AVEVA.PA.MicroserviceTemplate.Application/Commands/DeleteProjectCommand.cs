using MediatR;

namespace AVEVA.PA.MicroserviceTemplate.Application.Commands
{
    public class DeleteProjectCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
