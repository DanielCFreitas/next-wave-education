using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdQuery : IRequest<ResultViewModel<ProjectItemViewModel>>
{
    public int Id { get; set; }
}

public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectItemViewModel>>
{
    public GetProjectByIdHandler(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private readonly DevFreelaDbContext _dbContext;
    
    public async Task<ResultViewModel<ProjectItemViewModel>> Handle(GetProjectByIdQuery request,
        CancellationToken cancellationToken)
    {
        var project = await _dbContext.Projects
            .Include(project => project.Client)
            .Include(project => project.Freelancer)
            .Include(project => project.Comments)
            .SingleOrDefaultAsync(project => project.Id == request.Id);

        if (project is null)
            return ResultViewModel<ProjectItemViewModel>.Error("Projeto não existe");

        var model = ProjectItemViewModel.FromEntity(project);

        return ResultViewModel<ProjectItemViewModel>.Success(model);
    }
}