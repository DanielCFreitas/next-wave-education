using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject;

public class InsertProjectHandler(DevFreelaDbContext dbContext)
    : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
{
    public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
    {
        var project = request.ToEntity();

        dbContext.Projects.Add(project);
        await dbContext.SaveChangesAsync();

        return ResultViewModel<int>.Success(project.Id);
    }
}