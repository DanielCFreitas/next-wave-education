﻿using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertComment;

public class InsertCommentHandler :
    IRequestHandler<InsertCommentCommand, ResultViewModel>
{
    private readonly IProjectRepository _projectRepository;

    public InsertCommentHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
    {
        var projectExists = await _projectRepository.Exists(request.IdProject);

        if (!projectExists) return ResultViewModel.Error("Projeto não encontrado");

        var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);

        await _projectRepository.AddComment(comment);

        return ResultViewModel.Success();
    }
}