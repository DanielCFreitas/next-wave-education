using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.DeleteProject;

public class DeleteProjectCommand(int id) : IRequest<ResultViewModel>
{
    public int Id { get; set; } = id;
}