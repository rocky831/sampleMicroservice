using AutoMapper;
using MediatR;
using AVEVA.PA.MicroserviceTemplate.Application.Dtos;
using AVEVA.PA.MicroserviceTemplate.Application.Queries;
using AVEVA.PA.MicroserviceTemplate.Core.IConfiguration;
using Microsoft.Extensions.Logging;

namespace AVEVA.PA.MicroserviceTemplate.Application.Handlers
{
    public class GetAllProjectHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<ReadProjectDto>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<GetAllProjectHandler> logger;

        public GetAllProjectHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllProjectHandler> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<IEnumerable<ReadProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {

                this.logger.LogInformation($"Get all projects request recieved");
                var res = await this.unitOfWork.Projects.GetAllAsync();
                return this.mapper.Map<IEnumerable<ReadProjectDto>>(res);

        }
    }
}
