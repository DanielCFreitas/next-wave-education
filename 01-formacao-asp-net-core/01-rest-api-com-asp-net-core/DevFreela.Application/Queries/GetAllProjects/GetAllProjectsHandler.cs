using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, ResultViewModel<List<ProjectItemViewModel>>>
{
    public GetAllProjectsHandler(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private readonly DevFreelaDbContext _dbContext;

    public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetAllProjectsQuery request,
        CancellationToken cancellationToken)
    {
        var projects =
            await _dbContext.Projects
                .Include(project => project.Client)
                .Include(project => project.Freelancer)
                .Where(project =>
                    !project.IsDeleted && (project.Title.Contains(request.Search) ||
                                           project.Description.Contains(request.Search) ||
                                           request.Search.IsNullOrEmpty()))
                .Skip(request.Page * request.Size)
                .Take(request.Size)
                .ToListAsync();

        var model = projects
            .Select(ProjectItemViewModel.FromEntity)
            .ToList();

        return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
    }
}