using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence;

public class DevFreelaDbContext : DbContext
{
    public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options) : base(options) { }

    public DbSet<Project> Projects { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<UserSkill> UsersSkills { get; set; }
    public DbSet<ProjectComment> ProjectComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(skill => skill.Id);
        });

        modelBuilder.Entity<UserSkill>(entity =>
        {
            entity.HasKey(userSkill => userSkill.Id);

            entity.HasOne(userSkill => userSkill.Skill)
                .WithMany(skill => skill.UserSkills)
                .HasForeignKey(userSkill => userSkill.IdSkill)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ProjectComment>(entity =>
        {
            entity.HasKey(projectComment => projectComment.Id);
            
            entity.HasOne(projectComment => projectComment.Project)
                .WithMany(project => project.Comments)
                .HasForeignKey(projectComment => projectComment.IdProject)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(projectComment => projectComment.User)
                .WithMany(user => user.Comments)
                .HasForeignKey(projectComment => projectComment.IdUser)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(user => user.Id);

            entity.HasMany(user => user.Skills)
                .WithOne(userSkill => userSkill.User)
                .HasForeignKey(userSkill => userSkill.IdUser)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(project => project.Id);

            entity.HasOne(project => project.Freelancer)
                .WithMany(freelancer => freelancer.FreelanceProjects)
                .HasForeignKey(project => project.IdFreelancer)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(project => project.Client)
                .WithMany(client => client.OwnedProjects)
                .HasForeignKey(project => project.IdClient)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        base.OnModelCreating(modelBuilder);
    }
}