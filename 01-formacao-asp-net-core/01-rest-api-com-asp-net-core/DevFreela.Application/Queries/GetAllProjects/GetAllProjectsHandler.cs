using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, ResultViewModel<List<ProjectItemViewModel>>>
{
    private readonly IProjectRepository _projectRepository;

    public GetAllProjectsHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetAllProjectsQuery request,
        CancellationToken cancellationToken)
    {
        // var projects =
        //     await _dbContext.Projects
        //         .Include(project => project.Client)
        //         .Include(project => project.Freelancer)
        //         .Where(project =>
        //             !project.IsDeleted && (project.Title.Contains(request.Search) ||
        //                                    project.Description.Contains(request.Search) ||
        //                                    request.Search.IsNullOrEmpty()))
        //         .Skip(request.Page * request.Size)
        //         .Take(request.Size)
        //         .ToListAsync();

        var projects = await _projectRepository.GetAll();

        var model = projects
            .Select(ProjectItemViewModel.FromEntity)
            .ToList();

        return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
    }
}