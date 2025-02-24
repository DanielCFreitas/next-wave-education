namespace DevFreela.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string fullName, string email, DateTime birthDate, string password, string role, bool active = true) 
            : base()
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Active = active;
            Password = password;
            Role = role;
            
            Skills = [];
            OwnedProjects = [];
            Comments = [];
            FreelanceProjects = [];
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public bool Active { get; private set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public List<UserSkill> Skills { get; private set; }
        public List<ProjectComment> Comments { get; private set; }
        public List<Project> OwnedProjects { get; private set; }
        public List<Project> FreelanceProjects { get; private set; }

        public void UpdatePassword(string password)
        {
            Password = password;
        }
    }
}