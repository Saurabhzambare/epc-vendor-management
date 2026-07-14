namespace Warmup;

public class Employee
{
    public int Id { get; }
    public string Name { get; set; }
    public string Designation { get; set; }

    public Employee(int id, string name, string designation)
    {
        Id = id;
        Name = name;
        Designation = designation;
    }
}