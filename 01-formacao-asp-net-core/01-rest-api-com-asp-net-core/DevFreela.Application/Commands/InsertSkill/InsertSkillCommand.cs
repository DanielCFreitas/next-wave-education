using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.InsertSkill;

public class InsertSkillCommand(string description) : IRequest<ResultViewModel>
{
    public string Description { get; set; } = description;
}