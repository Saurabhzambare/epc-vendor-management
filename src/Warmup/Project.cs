namespace Warmup;

public enum ProjectStatus
{
    Planned,
    Active,
    Completed,
    OnHold
}

public class Project
{
    public int Id { get; }
    public string Name { get; set; }
    public int ManagerId { get; set; }
    public ProjectStatus Status { get; set; } = ProjectStatus.Planned;

    public Project(int id, string name, int managerId)
    {
        Id = id;
        Name = name;
        ManagerId = managerId;
    }
}