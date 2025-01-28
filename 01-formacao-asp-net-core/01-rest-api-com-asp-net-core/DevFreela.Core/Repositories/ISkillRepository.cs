using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;

public interface ISkillRepository
{
    Task<List<Skill>> GetAll();
    Task Add(Skill skill);
}