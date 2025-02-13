using DevFreela.Application.Commands.InsertProject;
using DevFreela.Application.Notification.ProjectCreated;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;
using NSubstitute;

namespace DevFreela.UnitTests.Application;

public class InsertProjectHandlerTests
{
    [Fact]
    public async Task InputDataAreOk_Insert_Success_NSubstitute()
    {
        // Arrange
        const int PROJECT_ID = 1;
        
        var repository = Substitute.For<IProjectRepository>();
        repository.Add(Arg.Any<Project>()).Returns(Task.FromResult(PROJECT_ID));

        var mediator = Substitute.For<IMediator>();

        var command = new InsertProjectCommand("Project A", "Descricao do projeto", 1, 2, 20000);
        var handler = new InsertProjectHandler(repository, mediator);
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(PROJECT_ID, result.Data);
        await repository.Received(1).Add(Arg.Any<Project>());
    }
}