using AutoMapper;
using MediatR;
using AVEVA.PA.MicroserviceTemplate.Core.IConfiguration;
using AVEVA.PA.MicroserviceTemplate.Application.Dtos;
using AVEVA.PA.MicroserviceTemplate.Application.Queries;
using Microsoft.Extensions.Logging;

namespace AVEVA.PA.MicroserviceTemplate.Application.Handlers
{
    public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ReadProjectDto>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<GetProjectByIdHandler> logger;

        public GetProjectByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetProjectByIdHandler> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ReadProjectDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {

            this.logger.LogInformation($"Get project request with id {request.ProjectID} recieved");
            var project = await this.unitOfWork.Projects.GetByIdAsync(request.ProjectID);
            return this.mapper.Map<ReadProjectDto>(project);
        }
    }
}
