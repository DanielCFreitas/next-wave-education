﻿using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using NSubstitute;

namespace DevFreela.UnitTests.Application;

public class DeleteProjectHandlerTests
{
    [Fact]
    public async Task ProjectExists_Delete_Success_NSubstitute()
    {
        // Arrange
        var project = new Project("Project A", "Descricao do projeto", 20000, 1, 2);

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
}