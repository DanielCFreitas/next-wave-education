using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.InsertUser;

public class InsertUserCommand(string fullName, DateTime birthDate, string email, string password, string role)
    : IRequest<ResultViewModel>
{
    public string FullName { get; set; } = fullName;
    public DateTime BirthDate { get; set; } = birthDate;
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
    public string Role { get; set; } = role;
}