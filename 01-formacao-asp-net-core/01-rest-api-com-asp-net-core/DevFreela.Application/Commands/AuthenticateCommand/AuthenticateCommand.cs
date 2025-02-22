using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.AuthenticateCommand;

public class AuthenticateCommand : IRequest<ResultViewModel>
{
    public string Email { get; set; }
    public string Password { get; set; }
}