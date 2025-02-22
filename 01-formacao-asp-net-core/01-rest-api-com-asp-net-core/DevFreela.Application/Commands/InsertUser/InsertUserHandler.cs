using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Auth;
using MediatR;

namespace DevFreela.Application.Commands.InsertUser;

public class InsertUserHandler :
    IRequestHandler<InsertUserCommand, ResultViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;


    public InsertUserHandler(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    public async Task<ResultViewModel> Handle(InsertUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = _authService.ComputeHash(request.Password);
        
        var user = new User(request.FullName, request.Email, request.BirthDate, passwordHash, request.Role);

        await _userRepository.Add(user);

        return ResultViewModel.Success();
    }
}