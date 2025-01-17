using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.UpdateProject;

public class UpdateProjectCommand(
    int idProject,
    string title,
    string description,
    int idClient,
    int idFreelancer,
    decimal totalCost)
    : IRequest<ResultViewModel>
{
    public int IdProject { get; set; } = idProject;
    public string Title { get; set; } = title;
    public string Description { get; set; } = description;
    public int IdClient { get; set; } = idClient;
    public int IdFreelancer { get; set; } = idFreelancer;
    public decimal TotalCost { get; set; } = totalCost;
}