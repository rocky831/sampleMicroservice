using MediatR;
using AVEVA.PA.MicroserviceTemplate.Application.Dtos;

namespace AVEVA.PA.MicroserviceTemplate.Application.Queries
{
    public class GetAllProjectsQuery : IRequest<IEnumerable<ReadProjectDto>>
    {
        
    }
}
