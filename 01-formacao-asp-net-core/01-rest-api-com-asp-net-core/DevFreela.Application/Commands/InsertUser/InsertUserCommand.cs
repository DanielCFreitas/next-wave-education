using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.InsertUser;

public class InsertUserCommand(string fullName, string email, DateTime birthDate) : IRequest<ResultViewModel>
{
    public string FullName { get; set; } = fullName;
    public string Email { get; set; } = email;
    public DateTime BirthDate { get; set; } = birthDate;
}