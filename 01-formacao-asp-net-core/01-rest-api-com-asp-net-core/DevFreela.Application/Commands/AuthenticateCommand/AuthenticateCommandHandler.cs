using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Auth;
using MediatR;

namespace DevFreela.Application.Commands.AuthenticateCommand;

public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, ResultViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public AuthenticateCommandHandler(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    public async Task<ResultViewModel> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = _authService.ComputeHash(request.Password);

        var user = await _userRepository.Authenticate(request.Email, passwordHash);

        if (user is null) return ResultViewModel.Error("Usuário ou senha inválidos");
        
        var jwtToken = _authService.GenerateToken(user.Email, user.Role);

        return ResultViewModel<string>.Success(jwtToken);
    }
}