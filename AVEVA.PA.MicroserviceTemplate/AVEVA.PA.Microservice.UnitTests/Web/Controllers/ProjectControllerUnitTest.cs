using Moq;
using Xunit;
using System;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using AVEVA.PA.MicroserviceTemplate.Application.Commands;
using AVEVA.PA.MicroserviceTemplate.Web.api.Controllers;
using AVEVA.PA.MicroserviceTemplate.Application.Dtos;
using AVEVA.PA.MicroserviceTemplate.Application.Queries;

namespace Microservice.UnitTests.Web.Controllers
{
    public class ProjectControllerUnitTest
    {
        private readonly Mock<IMediator> mediator;
        private readonly ILogger<ProjectController> logger = new MemoryLogger<ProjectController>();
        private readonly IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());

        public ProjectControllerUnitTest()
        {
            mediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task GetShouldReturnAllProjects()
        {
            //Arrange
            List<ReadProjectDto> projects = new List<ReadProjectDto>();
            projects.Add(GetReadProjectDto());

            mediator.Setup((x => x.Send(It.IsAny<GetAllProjectsQuery>(), CancellationToken.None))).ReturnsAsync(projects.AsEnumerable());
            var controller = new ProjectController(mediator.Object, logger, memoryCache);

            //Act
            var response = await controller.Get(CancellationToken.None);

            //Assert
            Assert.NotNull(response.Result);
        }

        [Fact]
        public async Task GetByIdShouldReturnProject()
        {
            //Arrange
            mediator.Setup((x => x.Send(It.IsAny<GetProjectByIdQuery>(), CancellationToken.None))).ReturnsAsync(GetReadProjectDto());
            var controller = new ProjectController((MediatR.IMediator)mediator.Object, logger, memoryCache);

            //Act
            var response = await controller.Get(1005, CancellationToken.None);

            //Assert
            Assert.NotNull(response.Result);
            
        }

        [Fact]
        public async Task AddProjectShouldAddProject()
        {
            //Arrange
            var projectAccepted = new ProjectAccepted(new Guid().ToString());
            mediator.Setup((x => x.Send(It.IsAny<CreateProjectCommand>(), CancellationToken.None))).ReturnsAsync(projectAccepted);
            var controller = new ProjectController(mediator.Object, logger, memoryCache);

            //Act
            var response = await controller.AddProject(new CreateProjectDto(), CancellationToken.None, "en-US") as AcceptedResult;

            //Assert
            Assert.NotNull(response);
            Assert.Equal(202, response?.StatusCode);
        }

        [Fact]
        public async Task DeleteShouldRemoveProject()
        {
            //Arrange
            mediator.Setup((x => x.Send(It.IsAny<DeleteProjectCommand>(), CancellationToken.None))).ReturnsAsync(true);
            var controller = new ProjectController(mediator.Object, logger, memoryCache);

            //Act
            var response = await controller.Delete(1005, CancellationToken.None) as OkObjectResult;

            //Assert
            Assert.NotNull(response);
            Assert.Equal(200, response?.StatusCode);
        }

        private static ReadProjectDto GetReadProjectDto()
        {
            ReadProjectDto project = new ReadProjectDto();
            project.ProjectID = 1005;
            project.AssetId = 1003;
            project.Name = "Mock Project";
            project.Description = "Mock desc";
            project.STATUSLASTUPDATE = DateTime.UtcNow;
            project.FilePath = "test";
            project.LastDeployUser = "testuser";
            project.LOCKEDUSER = "testuser";
            project.LASTMODIFIEDUSER = "testuser";
            project.STATUSID = 123;
            project.ARCHIVERTSID = 123;
            project.PROJECTTYPEID = 123;
            project.ALIASRTSID = 123;
            project.ParentTemplateID = 123;
            project.RATING = 123;
            project.CREATEDUSERNAME = "testname";
            project.TransientInterval = 123;
            project.CRITICALITY = 123;
            project.InstanceId = 123;
            project.SignalMinPercentValidValue = 123;
            project.SignalMinPercentValidSource = 1234;
            project.AutoFilterEnableValue = 123456;
            project.AutoFilterEnableSource = 456123;
            project.PollingIntervalSource = 12345;
            project.CriticalitySource = 789;
            project.ProjectGUID = "testguid";
            project.ProcessingMode = 12345;

            return project;
        }
    }
}
