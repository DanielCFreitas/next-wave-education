using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.UpdateProject;

public class UpdateProjectCommand(
    int idProject,
    string title,
    string description,
    decimal totalCost)
    : IRequest<ResultViewModel>
{
    public int IdProject { get; set; } = idProject;
    public string Title { get; set; } = title;
    public string Description { get; set; } = description;
    public decimal TotalCost { get; set; } = totalCost;
}