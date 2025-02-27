﻿using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.DeleteProject;

public class DeleteProjectHandler :
    IRequestHandler<DeleteProjectCommand, ResultViewModel>
{
    public const string PROJECT_NOT_FOUND_MESSAGE = "Projeto não encontrado";
    
    private readonly IProjectRepository _projectRepository;

    public DeleteProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ResultViewModel> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetById(request.Id);

        if (project is null) return ResultViewModel.Error(PROJECT_NOT_FOUND_MESSAGE);

        project.SetAsDeleted();

        await _projectRepository.Update(project);

        return ResultViewModel.Success();
    }
}