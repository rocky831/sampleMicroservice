

using AVEVA.PA.MicroserviceTemplate.Core.IConfiguration;
using Moq;

namespace Microservice.UnitTests.Application.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetMockUnitOfWork()
        {

            var mockProjectRepo = MockRepository.GetProjectRepository();

            // mock unit of work
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(uow => uow.Projects).Returns(mockProjectRepo.Object);

            return mockUnitOfWork;

        }
    }
}
