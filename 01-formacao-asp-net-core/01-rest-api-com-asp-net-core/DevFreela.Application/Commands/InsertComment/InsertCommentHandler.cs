using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.InsertComment;

public class InsertCommentHandler(DevFreelaDbContext dbContext) :
    IRequestHandler<InsertCommentCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
    {
        var project = await dbContext.Projects.SingleOrDefaultAsync(project => project.Id == request.IdProject);

        if (project is null) return ResultViewModel.Error("Projeto não encontrado");

        var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);

        dbContext.ProjectComments.Add(comment);
        await dbContext.SaveChangesAsync();

        return ResultViewModel.Success();
    }
}