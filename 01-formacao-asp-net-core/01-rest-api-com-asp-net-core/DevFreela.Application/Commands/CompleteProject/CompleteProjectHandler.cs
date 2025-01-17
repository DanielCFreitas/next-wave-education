using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.CompleteProject;

public class CompleteProjectHandler(DevFreelaDbContext dbContext)
    : IRequestHandler<CompleteProjectCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await dbContext.Projects.SingleOrDefaultAsync(project => project.Id == request.Id);

        if (project is null) return ResultViewModel.Error("Projeto não encontrado");

        project.Complete();
        dbContext.Projects.Update(project);
        await dbContext.SaveChangesAsync();
        
        return ResultViewModel.Success();
    }
}