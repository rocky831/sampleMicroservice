using System;
using Xunit;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AVEVA.PA.MicroserviceTemplate.Core.IConfiguration;
using AVEVA.PA.MicroserviceTemplate.Infrastructure.Repositories;
using AVEVA.PA.DataAccess.Models;
using AVEVA.PA.Exceptions;

namespace Microservice.UnitTests.Infrastructure.ProjectRepositoryTests
{
    public class ProjectRepositoryTest : BaseDbContextTest
    {
        [Fact]
        public async Task GetProjectById_ValidProjectId_ReturnProject()
        {
            int projectId = 1005;
            // Arrange
            var mockRepository = new Mock<ProjectRepository>(base.DbContext);
            mockRepository.Setup(m => m.GetByIdAsync(projectId)).Returns(It.IsAny<Task<Project>>).Verifiable();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.Projects).Returns(mockRepository.Object);

            // Act        
            var repository = new ProjectRepository(base.DbContext);
            var project = await Task.Run(() => repository.GetByIdAsync(projectId));

            // Assert
            Assert.NotNull(project);
            Assert.Equal("Mock Project", project.Name);
            Assert.Equal(1003, project.AssetId);
            Assert.Equal("Mock desc", project.Description);
        }


        [Fact]
        public async Task GetProjectById_InvalidProjectId_ReturnNotFoundException()
        {
            int projectId = -100;
            // Arrange
            var mockRepository = new Mock<ProjectRepository>(base.DbContext);
            mockRepository.Setup(m => m.GetByIdAsync(projectId)).Returns(It.IsAny<Task<Project>>).Verifiable();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.Projects).Returns(mockRepository.Object);

            // Act        
            var repository = new ProjectRepository(base.DbContext);
           
            await Assert.ThrowsAsync<NotFoundException>(async () => await repository.GetByIdAsync(projectId));
   
        }

        [Fact]
        public async Task GetAllProjects_List_ReturnProjects()
        {
            // Arrange
            var mockRepository = new Mock<ProjectRepository>(base.DbContext);
            mockRepository.Setup(m => m.GetAllAsync()).Returns(It.IsAny<Task<IReadOnlyCollection<Project>>>).Verifiable();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.Projects).Returns(mockRepository.Object);

            // Act        
            var repository = new ProjectRepository(base.DbContext);
            var projects = await Task.Run(() => repository.GetAllAsync());

            // Assert
            Assert.True(projects.Any());
            var project = projects.SingleOrDefault(x => x.ProjectId == 1006);
            Assert.NotNull(project);
            Assert.Equal("Mock Project2", project?.Name);
            Assert.Equal(1003, project?.AssetId);
            Assert.Equal("Mock desc2", project?.Description);
        }

        [Fact]
        public async Task AddProject_ValidProjectData_NewProjectShouldAdded()
        {
            var projectEntity = new Project
            {
                AssetId = 1003,
                Name = "New Project",
                Description = "Project Description"
            };

            // Arrange
            var mockRepository = new Mock<ProjectRepository>(base.DbContext);
            mockRepository.Setup(m => m.AddAsync(projectEntity)).Returns(It.IsAny<Task<Project>>).Verifiable();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.Projects).Returns(mockRepository.Object);

            // Act        
            var repository = new ProjectRepository(base.DbContext);
            var result = await Task.Run(() => repository.AddAsync(projectEntity));

            await mockUnitOfWork.Object.SaveAsync();

            // Assert
            Assert.True(result.ProjectId > 0);
            var project = await Task.Run(() => repository.GetByIdAsync(result.ProjectId));
            Assert.NotNull(project);
            Assert.Equal("New Project", project.Name);
            Assert.Equal(1003, project.AssetId);
            Assert.Equal("Project Description", project.Description);
        }

        [Fact]
        public async Task UpdateProject_ValidProjectData_ProjectShouldUpdated()
        {
            int projectId = 1005;
            // Arrange
            var mockRepository = new Mock<ProjectRepository>(base.DbContext);
            mockRepository.Setup(m => m.GetByIdAsync(projectId)).Returns(It.IsAny<Task<Project>>).Verifiable();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.Projects).Returns(mockRepository.Object);

            // Act        
            var repository = new ProjectRepository(base.DbContext);
            var project = await Task.Run(() => repository.GetByIdAsync(projectId));

            project.Name = "updated project";
            project.Description = "updated description";

            // Arrange
            mockRepository.Setup(m => m.UpdateAsync(project, project.ProjectId)).Returns(It.IsAny<Task<Project>>).Verifiable();
            var updatedProject = await Task.Run(() => repository.UpdateAsync(project, project.ProjectId));
            await mockUnitOfWork.Object.SaveAsync();

            Assert.NotNull(updatedProject);
            Assert.Equal(project.Name, updatedProject.Name);
            Assert.Equal(project.Description, updatedProject.Description);
        }

        [Fact]
        public async Task DeleteProject_ValidProjectId_ProjectShouldDeleted()
        {
            int projectId = 1005;
            // Arrange
            var mockRepository = new Mock<ProjectRepository>(base.DbContext);
            mockRepository.Setup(m => m.DeleteAsync(projectId)).Returns(It.IsAny<Task<bool>>).Verifiable();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.Projects).Returns(mockRepository.Object);

            // Act        
            var repository = new ProjectRepository(base.DbContext);
            var result = await Task.Run(() => repository.DeleteAsync(projectId));

            await mockUnitOfWork.Object.SaveAsync();

            // Assert
            Assert.True(result);
        }
    }
}
