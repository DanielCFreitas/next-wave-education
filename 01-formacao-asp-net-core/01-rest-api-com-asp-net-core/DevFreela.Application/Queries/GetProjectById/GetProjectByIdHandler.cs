using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectItemViewModel>>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectByIdHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ResultViewModel<ProjectItemViewModel>> Handle(GetProjectByIdQuery request,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetDetailsById(request.Id);

        if (project is null)
            return ResultViewModel<ProjectItemViewModel>.Error("Projeto não existe");

        var model = ProjectItemViewModel.FromEntity(project);

        return ResultViewModel<ProjectItemViewModel>.Success(model);
    }
}