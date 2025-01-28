using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertSkillsToUser;

public class InsertSkillsToUserHandler
    : IRequestHandler<InsertSkillsToUserCommand, ResultViewModel>
{
    private readonly IUserRepository _userRepository;

    public InsertSkillsToUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResultViewModel> Handle(InsertSkillsToUserCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.Exists(request.Id);

        if (!userExists) return ResultViewModel.Error("O usuário não existe");

        var userSkills = request.SkillsId
            .Select(skillId => new UserSkill(request.Id, skillId));

        await _userRepository.AddSkills(userSkills);

        return ResultViewModel.Success();
    }
}