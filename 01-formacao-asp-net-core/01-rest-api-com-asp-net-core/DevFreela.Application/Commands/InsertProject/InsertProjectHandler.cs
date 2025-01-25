using DevFreela.Application.Models;
using DevFreela.Application.Notification.ProjectCreated;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject;

public class InsertProjectHandler
    : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
{
    
    private readonly DevFreelaDbContext _dbContext;
    private readonly IMediator _mediator;
    
    public InsertProjectHandler(DevFreelaDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }
    
    public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
    {
        var project = request.ToEntity();

        _dbContext.Projects.Add(project);
        await _dbContext.SaveChangesAsync();

        var projectCreated = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);
        await _mediator.Publish(projectCreated);

        return ResultViewModel<int>.Success(project.Id);
    }
}