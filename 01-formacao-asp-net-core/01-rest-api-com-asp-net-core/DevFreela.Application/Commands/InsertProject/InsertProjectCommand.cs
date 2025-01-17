using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject;

public class InsertProjectCommand(string title, string description, int idClient, int idFreelancer, decimal totalCost) :
    IRequest<ResultViewModel<int>>
{
    public string Title { get; set; } = title;
    public string Description { get; set; } = description;
    public int IdClient { get; set; } = idClient;
    public int IdFreelancer { get; set; } = idFreelancer;
    public decimal TotalCost { get; set; } = totalCost;
    
    
    public Project ToEntity()
        => new(Title, Description, TotalCost, IdClient, IdFreelancer);
}