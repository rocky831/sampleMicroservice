using Moq;
using AutoMapper;
using Xunit;
using System.Threading;
using Microsoft.Extensions.Logging;
using AVEVA.PA.MicroserviceTemplate.Application.Handlers;
using AVEVA.PA.MicroserviceTemplate.Application.ProfileMapping;
using AVEVA.PA.MicroserviceTemplate.Application.Queries;
using AVEVA.PA.MicroserviceTemplate.Core.IConfiguration;
using Microservice.UnitTests.Web.Controllers;
using Microservice.UnitTests.Application.Mocks;
using System.Threading.Tasks;

namespace Microservice.UnitTests.Application.Handlers
{
    public class GetProjectByIdHandlerTest
    {
        public readonly IMapper mapper;
        public readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly ILogger<GetProjectByIdHandler> logger = new MemoryLogger<GetProjectByIdHandler>();

        public GetProjectByIdHandlerTest()
        {
            var mapperConfig = new MapperConfiguration(c => {
                c.AddProfile<ProfileMapping>();
            });

            mapper = mapperConfig.CreateMapper();

            mockUnitOfWork = MockUnitOfWork.GetMockUnitOfWork();
        }


        [Fact]
        public async Task Given_Wrong_Id_Should_Not_Return_Project()
        {
            int projectId = 1;

            var handler = new GetProjectByIdHandler(mockUnitOfWork.Object, mapper, logger);

            var result = await handler.Handle(new GetProjectByIdQuery(projectId), CancellationToken.None);

            Assert.Null(result);
        }

        [Fact]
        public async Task Given_Correct_Id_Should_Return_Project()
        {
            int projectId = 1005;

            var handler = new GetProjectByIdHandler(mockUnitOfWork.Object, mapper, logger);

            var result = await handler.Handle(new GetProjectByIdQuery(projectId), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(result.ProjectID, projectId);
        }
    }
}
