using Moq;
using System;
using System.Linq;
using System.Collections.Generic;
using AVEVA.PA.MicroserviceTemplate.Core.IRepositories;
using AVEVA.PA.DataAccess.Models;

namespace Microservice.UnitTests.Application.Mocks
{
    public static class MockRepository
    {
        public static Mock<IProjectRepository> GetProjectRepository()
        {
            Project project = new Project
            {
                ProjectId = 1005,
                AssetId = 1003,
                Name = "Mock Project",
                Description = "Mock desc",
                Statuslastupdate = DateTime.UtcNow,
                FilePath = "test",
                LastDeployUser = "testuser",
                Lockeduser = "testuser",
                Lastmodifieduser = "testuser",
                Statusid = 123,
                Archivertsid = 123,
                Projecttypeid = 123,
                Aliasrtsid = 123,
                ParentTemplateId = 123,
                Rating = 123,
                Createdusername = "testname",
                TransientInterval = 123,
                Criticality = 123,
                InstanceId = 123,
                SignalMinPercentValidValue = 123,
                SignalMinPercentValidSource = 1234,
                AutoFilterEnableValue = 123456,
                AutoFilterEnableSource = 456123,
                PollingIntervalSource = 12345,
                CriticalitySource = 789,
                ProjectGuid = "testguid",
                ProcessingMode = 12345
            };

            List<Project> projects = new List<Project>();
            projects.Add(project);

            // mock project repo
            var mockRepository = new Mock<IProjectRepository>();

            mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(projects);

            mockRepository.Setup(x => x.AddAsync(It.IsAny<Project>())).ReturnsAsync((Project project) =>
            {
                projects.Add(project);
                return project;
            });

            mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int ProjectId) => {
                return projects.FirstOrDefault(x => x.ProjectId == ProjectId);

            });

            return mockRepository;
        }
    }
}
