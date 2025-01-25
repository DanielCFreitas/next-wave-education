using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.InsertSkillsToUser;

public class InsertSkillsToUserCommand(int[] skillsId, int id) : IRequest<ResultViewModel>
{
    public int[] SkillsId { get; set; } = skillsId;
    public int Id { get; set; } = id;
}