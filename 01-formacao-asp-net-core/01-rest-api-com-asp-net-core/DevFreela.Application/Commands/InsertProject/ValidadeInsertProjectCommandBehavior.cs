using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject;

public class ValidadeInsertProjectCommandBehavior :
    IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>
{
    private readonly IUserRepository _userRepository;

    public ValidadeInsertProjectCommandBehavior(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request,
        RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
    {
        var clientExists = await _userRepository.Exists(request.IdClient);
        var freelancerExists = await _userRepository.Exists(request.IdFreelancer);

        if (!clientExists || !freelancerExists)
            return ResultViewModel<int>.Error("Cliente ou Freelancer inválidos");

        return await next();
    }
}