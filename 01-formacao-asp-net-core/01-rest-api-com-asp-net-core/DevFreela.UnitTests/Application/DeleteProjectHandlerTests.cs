using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Fakes;
using FluentAssertions;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application;

public class DeleteProjectHandlerTests
{
    [Fact]
    public async Task ProjectExists_Delete_Success_NSubstitute()
    {
        // Arrange
        var project = FakeDataHelper.CreateFakeProject();
            
        var repository = Substitute.For<IProjectRepository>();
        repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
        repository.Update(Arg.Any<Project>()).Returns(Task.CompletedTask);

        var handler = new DeleteProjectHandler(repository);
        var command = new DeleteProjectCommand(1);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        await repository.Received(1).GetById(1);
        await repository.Received(1).Update(Arg.Any<Project>());
    }
    
    [Fact]
    public async Task ProjectExists_Delete_Success_Moq()
    {
        // Arrange
        var project = FakeDataHelper.CreateFakeProject();
        
        // ==================== Primeira forma de instanciar usando o MOQ ========================
        var mockRepository = new Mock<IProjectRepository>();
        mockRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
            .Returns(Task.FromResult((Project?)project));
        mockRepository.Setup(repository => repository.Update(It.IsAny<Project>())).Returns(Task.CompletedTask);
        
        // ===================== Segunda forma de instanciar usando o MOQ ========================
        // var repository = Mock.Of<IProjectRepository>(repository =>
        //     repository.GetById(It.IsAny<int>()) == Task.FromResult(project) &&
        //     repository.Update(It.IsAny<Project>()) == Task.CompletedTask);

        var handler = new DeleteProjectHandler(mockRepository.Object);
        var command = new DeleteProjectCommand(1);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        result.IsSuccess.Should().BeTrue();
        
        mockRepository.Verify(repositoryVerify => repositoryVerify.GetById(It.IsAny<int>()), Times.Once);
        mockRepository.Verify(repositoryVerify => repositoryVerify.Update(It.IsAny<Project>()), Times.Once);
        
        // Mock.Get(repository).Verify(repositoryVerify => repositoryVerify.GetById(It.IsAny<int>()), Times.Once);
        // Mock.Get(repository).Verify(repositoryVerify => repositoryVerify.Update(It.IsAny<Project>()), Times.Once);
    }

    [Fact]
    public async Task ProjectDoesNotExist_Delete_Error_NSubstitute()
    {
        // Arrange
        var repository = Substitute.For<IProjectRepository>();
        repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)null));

        var handler = new DeleteProjectHandler(repository);
        var command = new DeleteProjectCommand(1);
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DeleteProjectHandler.PROJECT_NOT_FOUND_MESSAGE, result.Message);
        await repository.Received(1).GetById(1);
        await repository.DidNotReceive().Update(Arg.Any<Project>());
    }
    
    [Fact]
    public async Task ProjectDoesNotExist_Delete_Error_Moq()
    {
        // Arrange
        var repository = Mock.Of<IProjectRepository>(repository =>
            repository.GetById(It.IsAny<int>()) == Task.FromResult((Project?)null));

        var handler = new DeleteProjectHandler(repository);
        var command = new DeleteProjectCommand(1);
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        result.IsSuccess.Should().BeFalse();
        
        Assert.Equal(DeleteProjectHandler.PROJECT_NOT_FOUND_MESSAGE, result.Message);
        result.Message.Should().Be(DeleteProjectHandler.PROJECT_NOT_FOUND_MESSAGE);
        
        Mock.Get(repository).Verify(repositoryVerify => repositoryVerify.GetById(It.IsAny<int>()), Times.Once);
        Mock.Get(repository).Verify(repositoryVerify => repositoryVerify.Update(It.IsAny<Project>()), Times.Never);
    }
}