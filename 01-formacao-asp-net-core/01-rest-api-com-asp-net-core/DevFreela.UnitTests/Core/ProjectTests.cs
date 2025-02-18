using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using FluentAssertions;
using NSubstitute.ExceptionExtensions;

namespace DevFreela.UnitTests.Core;

public class ProjectTests
{
    [Fact]
    public void ProjectIsCreated_Start_Success()
    {
        // Arrange
        var project = new Project("Projeto A", "Descricao do Projecto", 1000, 1, 2);

        // Act
        project.Start();

        // Assert
        Assert.Equal(ProjectStateEnum.InProgress, project.Status);
        project.Status.Should().Be(ProjectStateEnum.InProgress);
        
        Assert.NotNull(project.StartedAt);
        project.StartedAt.Should().NotBeNull();
    }

    [Fact]
    public void ProjectIsInInvalidState_Start_ThrowsException()
    {
        // Arrange
        var project = new Project("Projeto A", "Descricao do Projecto", 1000, 1, 2);
        project.Start();
        
        // Act + Assert
        Action start = () => project.Start();
        
        var exceptions = Assert.Throws<InvalidOperationException>(start);
        Assert.Equal(Project.INVALID_STATE_MESSAGE, exceptions.Message);
        
        start.Should()
            .Throw<InvalidOperationException>()
            .WithMessage(Project.INVALID_STATE_MESSAGE);
    }
}