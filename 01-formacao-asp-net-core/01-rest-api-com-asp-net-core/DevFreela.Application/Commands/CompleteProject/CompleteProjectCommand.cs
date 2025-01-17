using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.CompleteProject;

public class CompleteProjectCommand(int id) : IRequest<ResultViewModel>
{
    public int Id { get; set; } = id;
}