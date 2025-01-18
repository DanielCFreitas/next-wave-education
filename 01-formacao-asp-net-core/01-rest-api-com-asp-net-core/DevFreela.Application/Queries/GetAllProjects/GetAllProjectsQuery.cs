using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsQuery(string search, int page, int size)
    : IRequest<ResultViewModel<List<ProjectItemViewModel>>>
{
    public string Search { get; set; } = search;
    public int Page { get; set; } = page;
    public int Size { get; set; } = size;
}