﻿using DevFreela.Core.Entities;

namespace DevFreela.Application.Models;

public class UserViewModel
{
    public UserViewModel(string fullName, string email, DateTime birthDate, IEnumerable<string> skills)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        Skills = skills;
    }

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public IEnumerable<string> Skills { get; private set; }

    public static UserViewModel FromEntity(User user)
        => new(user.FullName, user.Email, user.BirthDate,
            user.Skills.Select(userSkills => userSkills.Skill.Description));
}