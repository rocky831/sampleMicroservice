using AutoMapper;
using AVEVA.PA.DataAccess.Models;
using AVEVA.PA.MicroserviceTemplate.Application.Commands;
using AVEVA.PA.MicroserviceTemplate.Application.Dtos;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AVEVA.PA.MicroserviceTemplate.Application.Handlers
{
    public class CreateProjecthandler : IRequestHandler<CreateProjectCommand, ProjectAccepted>
    {
        private readonly IMapper mapper;
        private readonly IBus bus;
        private readonly ILogger<CreateProjecthandler> logger;
        private readonly IMemoryCache memoryCache;

        public CreateProjecthandler(IMapper mapper, IBus bus,ILogger<CreateProjecthandler> logger, IMemoryCache memoryCache)
        {           
            this.mapper = mapper;
            this.bus = bus;
            this.logger = logger;
            this.memoryCache = memoryCache;
        }
        public async Task<ProjectAccepted> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = this.mapper.Map<Project>(request.Project);
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


            this.logger.LogInformation("Publishing message to event bus", JsonConvert.SerializeObject(project));
            await this.bus.Publish(project, cancellationToken);
            this.memoryCache.Set(project.ProjectGuid.ToString(), "InProgress");
            return new ProjectAccepted(project.ProjectGuid);
        }
    }
}
