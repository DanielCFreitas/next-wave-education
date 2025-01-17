using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.StartProject;

public class StartProjectHandler(DevFreelaDbContext dbContext) :
    IRequestHandler<StartProjectCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(StartProjectCommand request, CancellationToken cancellationToken)
    {
        var projeto = await dbContext.Projects.SingleOrDefaultAsync(project => project.Id == request.Id);

        if (projeto == null) return ResultViewModel.Error("Projeto não encontrado");
        
        projeto.Start();
        dbContext.Projects.Update(projeto);
        await dbContext.SaveChangesAsync();
        
        return ResultViewModel.Success();
    }
}