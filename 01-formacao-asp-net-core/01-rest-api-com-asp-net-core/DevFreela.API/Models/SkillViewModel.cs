using DevFreela.API.Entities;

namespace DevFreela.API.Models;

public class SkillViewModel
{
    public SkillViewModel(string description, IEnumerable<string> userNames)
    {
        Description = description;
        UserNames = userNames;
    }
    
    public string Description { get; private set; }
    public IEnumerable<string> UserNames { get; private set; }

    public static SkillViewModel FromEntity(Skill skill)
    {
        var usersWithSkill = skill.UserSkills
            .Select(user => user.User.FullName);
        
        return new(skill.Description, usersWithSkill);
    }
}