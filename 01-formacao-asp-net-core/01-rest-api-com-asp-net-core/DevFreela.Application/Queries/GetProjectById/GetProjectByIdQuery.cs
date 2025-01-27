using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdQuery : IRequest<ResultViewModel<ProjectItemViewModel>>
{
    public int Id { get; set; }
}