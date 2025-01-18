using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.InsertSkill;

public class InsertSkillHandler(DevFreelaDbContext dbContext) : IRequestHandler<InsertSkillCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = new Skill(request.Description);
        
        dbContext.Skills.Add(skill);
        await dbContext.SaveChangesAsync();
        
        return ResultViewModel.Success();
    }
}