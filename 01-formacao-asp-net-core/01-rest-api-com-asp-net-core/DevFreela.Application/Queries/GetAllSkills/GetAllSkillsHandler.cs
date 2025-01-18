using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllSkills;

public class GetAllSkillsHandler(DevFreelaDbContext dbContext) :
    IRequestHandler<GetAllSkillsQuery, ResultViewModel<List<SkillViewModel>>>
{
    public async Task<ResultViewModel<List<SkillViewModel>>> Handle(GetAllSkillsQuery request,
        CancellationToken cancellationToken)
    {
        var skills = await dbContext.Skills
            .Include(skill => skill.UserSkills)
            .ToListAsync();

        var skillsViewModel = skills.Select(SkillViewModel.FromEntity).ToList();

        return ResultViewModel<List<SkillViewModel>>.Success(skillsViewModel);
    }
}