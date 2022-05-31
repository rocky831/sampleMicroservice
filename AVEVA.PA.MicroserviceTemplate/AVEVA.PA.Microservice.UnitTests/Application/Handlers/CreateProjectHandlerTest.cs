using Moq;
using Xunit;
using MassTransit;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using AVEVA.PA.MicroserviceTemplate.Application.Commands;
using AVEVA.PA.MicroserviceTemplate.Application.Dtos;
using AVEVA.PA.MicroserviceTemplate.Application.Handlers;
using AVEVA.PA.MicroserviceTemplate.Application.ProfileMapping;
using AVEVA.PA.MicroserviceTemplate.Core.IConfiguration;
using Microservice.UnitTests.Application.Mocks;
using Microservice.UnitTests.Web.Controllers;

namespace Microservice.UnitTests.Application.Handlers
{
    public class CreateProjectHandlerTest
    {
        public readonly IMapper mapper;
        public readonly Mock<IUnitOfWork> mockUnitOfWork;
        public readonly Mock<IBus> bus;
        private readonly ILogger<CreateProjecthandler> logger = new MemoryLogger<CreateProjecthandler>();

        private readonly IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());
        public CreateProjectHandlerTest()
        {
            var mapperConfig = new MapperConfiguration(c => {
                c.AddProfile<ProfileMapping>();
            });

            mapper = mapperConfig.CreateMapper();

            mockUnitOfWork = MockUnitOfWork.GetMockUnitOfWork();
            bus = new Mock<IBus>();
        }


        [Fact]
        public async Task Should_Create_Project_And_Return_New_Project()
        {
            var handler = new CreateProjecthandler(mapper, bus.Object, logger, memoryCache);

            //TODO: Refactor this unit test
            string projectName = "Test Project";

            var createProjectDto = new CreateProjectDto
            {
                AssetId = 1007,
                Name = projectName,
                Description = "Test desc"
            };

            var result = await handler.Handle(new CreateProjectCommand() { Project = createProjectDto }, CancellationToken.None);

            Assert.NotNull(result);
            Assert.NotNull(result.ProjectGuid);
        }
    }
}
