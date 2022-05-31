using Moq;
using Xunit;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AVEVA.PA.MicroserviceTemplate.Core.IConfiguration;
using AVEVA.PA.MicroserviceTemplate.Application.Handlers;
using AVEVA.PA.MicroserviceTemplate.Application.ProfileMapping;
using AVEVA.PA.MicroserviceTemplate.Application.Queries;
using Microservice.UnitTests.Web.Controllers;
using Microservice.UnitTests.Application.Mocks;

namespace Microservice.UnitTests.Application.Handlers
{
    public class GetAllProjectsHandlerTest
    {
        public readonly IMapper mapper;
        public readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly ILogger<GetAllProjectHandler> logger = new MemoryLogger<GetAllProjectHandler>();

        public GetAllProjectsHandlerTest()
        {
            var mapperConfig = new MapperConfiguration(c => {
                c.AddProfile<ProfileMapping>();
            });

            mapper = mapperConfig.CreateMapper();

            mockUnitOfWork = MockUnitOfWork.GetMockUnitOfWork();
        }

        [Fact]
        public async Task Should_Return_All_Projects()
        {
            var handler = new GetAllProjectHandler(mockUnitOfWork.Object, mapper, logger);

            var result = await handler.Handle(new GetAllProjectsQuery(), CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Count() > 0);
        }
    }
}
