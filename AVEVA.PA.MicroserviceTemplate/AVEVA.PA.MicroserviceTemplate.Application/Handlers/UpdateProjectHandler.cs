using AutoMapper;
using AVEVA.PA.DataAccess.Models;
using AVEVA.PA.MicroserviceTemplate.Application.Commands;
using AVEVA.PA.MicroserviceTemplate.Application.Dtos;
using AVEVA.PA.MicroserviceTemplate.Core.IConfiguration;
using AVEVA.PA.MicroserviceTemplate.Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AVEVA.PA.MicroserviceTemplate.Application.Handlers
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ReadProjectDto>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<UpdateProjectHandler> logger;

        public UpdateProjectHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateProjectHandler> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<ReadProjectDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = this.mapper.Map<Project>(request.Project);
            project.ProjectId = request.Id;
            project.Statuslastupdate = DateTime.UtcNow;
            project.FilePath = "test";
            project.LastDeployUser = "testuser";
            project.Lockeduser = "testuser";
            project.Lastmodifieduser = "testuser";
            project.Statusid = 123;
            project.Archivertsid = 123;
            project.Projecttypeid = 123;
            project.Aliasrtsid = 123;
            project.ParentTemplateId = 123;
            project.Rating = 123;
            project.Createdusername = "testname";
            project.TransientInterval = 123;
            project.Criticality = 123;
            project.InstanceId = 123;
            project.SignalMinPercentValidValue = 123;
            project.SignalMinPercentValidSource = 1234;
            project.AutoFilterEnableValue = 123456;
            project.AutoFilterEnableSource = 456123;
            project.PollingIntervalSource = 12345;
            project.CriticalitySource = 789;
            project.ProjectGuid = Guid.NewGuid().ToString();
            project.ProcessingMode = 12345;

            this.logger.LogInformation($"Update project request with id {request.Id} received");
            var updatedProject = await this.unitOfWork.Projects.UpdateAsync(project, request.Id);
            await this.unitOfWork.SaveAsync();

            return this.mapper.Map<ReadProjectDto>(updatedProject);
        }
    }
}
