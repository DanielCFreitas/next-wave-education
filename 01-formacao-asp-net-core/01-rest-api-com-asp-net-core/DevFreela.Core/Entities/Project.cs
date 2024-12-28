using DevFreela.Core.Enums;

namespace DevFreela.Core.Entities;

public class Project : BaseEntity
{
    protected Project()
    {
    }

    public Project(string title, string description, decimal totalCost, int idClient, int idFreelancer)
        : base()
    {
        Title = title;
        Description = description;
        TotalCost = totalCost;
        IdClient = idClient;
        IdFreelancer = idFreelancer;

        Status = ProjectStateEnum.Created;
        Comments = [];
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal TotalCost { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public ProjectStateEnum Status { get; private set; }
    public List<ProjectComment> Comments { get; set; }


    public int IdClient { get; private set; }
    public User Client { get; private set; }

    public int IdFreelancer { get; private set; }
    public User Freelancer { get; private set; }

    public void Cancel()
    {
        if (Status is ProjectStateEnum.InProgress or ProjectStateEnum.Suspended)
            Status = ProjectStateEnum.Cancelled;
    }

    public void Start()
    {
        if (Status is not ProjectStateEnum.Created) return;

        Status = ProjectStateEnum.InProgress;
        StartedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        if (Status is not (ProjectStateEnum.PaymentPending or ProjectStateEnum.InProgress)) return;

        Status = ProjectStateEnum.Completed;
        CompletedAt = DateTime.UtcNow;
    }

    public void SetPaymentPending()
    {
        if (Status is ProjectStateEnum.InProgress)
            Status = ProjectStateEnum.PaymentPending;
    }

    public void Update(string title, string description, decimal totalCost)
    {
        Title = title;
        Description = description;
        TotalCost = totalCost;
    }
}