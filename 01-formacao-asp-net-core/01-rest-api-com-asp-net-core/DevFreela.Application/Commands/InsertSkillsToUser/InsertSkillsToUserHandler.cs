using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.InsertSkillsToUser;

public class InsertSkillsToUserHandler(DevFreelaDbContext dbContext)
    : IRequestHandler<InsertSkillsToUserCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(InsertSkillsToUserCommand request, CancellationToken cancellationToken)
    {
        var userSkills = request.SkillsId
            .Select(skillId => new UserSkill(request.Id, skillId));

        dbContext.UsersSkills.AddRange(userSkills);
        await dbContext.SaveChangesAsync();

        return ResultViewModel.Success();
    }
}