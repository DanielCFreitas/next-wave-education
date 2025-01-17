using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.DeleteProject;

public class DeleteProjectHandler(DevFreelaDbContext dbContext) : 
    IRequestHandler<DeleteProjectCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await dbContext.Projects.SingleOrDefaultAsync(project => project.Id == request.Id);

        if (project is null) return ResultViewModel.Error("Projeto não encontrado");

        project.SetAsDeleted();
        dbContext.Projects.Update(project);
        await dbContext.SaveChangesAsync();
        
        return ResultViewModel.Success();
    }
}