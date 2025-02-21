using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertUser;

public class InsertUserHandler :
    IRequestHandler<InsertUserCommand, ResultViewModel>
{
    private readonly IUserRepository _userRepository;

    public InsertUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResultViewModel> Handle(InsertUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.FullName, request.Email, request.BirthDate, request.Password, request.Role);

        await _userRepository.Add(user);

        return ResultViewModel.Success();
    }
}