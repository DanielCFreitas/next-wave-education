using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.UpdateProject;

public class UpdateProjectHandler(DevFreelaDbContext dbContext) : IRequestHandler<UpdateProjectCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await dbContext.Projects.SingleOrDefaultAsync(project => project.Id == request.IdProject);

        if (project is null) return ResultViewModel.Error("Projeto não encontrado");

        project.Update(request.Title, request.Description, request.TotalCost);

        dbContext.Projects.Update(project);
        await dbContext.SaveChangesAsync();

        return ResultViewModel.Success();
    }
}