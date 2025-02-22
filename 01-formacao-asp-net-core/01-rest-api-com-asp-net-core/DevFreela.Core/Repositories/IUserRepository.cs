using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByIdWithSkills(int id);
    Task Add(User user);
    Task AddSkills(IEnumerable<UserSkill> userSkills);
    Task<bool> Exists(int id);
    Task<User?> Authenticate(string email, string password);
}