﻿using DevFreela.Application.Models;
using DevFreela.Application.Notification.ProjectCreated;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject;

public class InsertProjectHandler
    : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMediator _mediator;
    
    public InsertProjectHandler(IProjectRepository projectRepository, IMediator mediator)
    {
        _projectRepository = projectRepository;
        _mediator = mediator;
    }
    
    public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
    {
        var project = request.ToEntity();

        var projectId = await _projectRepository.Add(project);

        var projectCreated = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);
        await _mediator.Publish(projectCreated);

        return ResultViewModel<int>.Success(projectId);
    }
}