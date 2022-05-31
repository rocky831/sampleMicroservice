using MediatR;
using AVEVA.PA.MicroserviceTemplate.Application.Dtos;

namespace AVEVA.PA.MicroserviceTemplate.Application.Queries
{
    public class GetProjectByIdQuery : IRequest<ReadProjectDto>
    {
        public int ProjectID { get; set; }

        public GetProjectByIdQuery(int projectID)
        {
            this.ProjectID = projectID;
        }
    }
}
