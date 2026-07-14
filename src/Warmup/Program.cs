using Warmup;



//vendors
var vendors = new List<Vendor>
{
    new Vendor("VEN-001", "Apex Civil Works", VendorCategory.Civil),
    new Vendor("VEN-002", "Bharat Electricals", VendorCategory.Electrical),
    new Vendor("VEN-003", "Meridian IT Solutions", VendorCategory.ItServices),
};

vendors[1].IsActive = false;
vendors[2].ContactEmail = "sales@meridian-it.example";

// 1. Filter and sort active vendors
var activeVendors = vendors
    .Where(v => v.IsActive)
    .OrderBy(v => v.Name);

vendors.Add(
    new Vendor(
        "VEN-004",
        "Zenith Mechanical",
        VendorCategory.Mechanical));

Console.WriteLine("Active vendors:");

foreach (var vendor in activeVendors)
{
    Console.WriteLine(
        $"  {vendor.VendorCode}: {vendor.Name} ({vendor.Category})");
}

// 2. Create simple summary text for every vendor
var summaryLines = vendors
    .Select(v => $"{v.Name} — {(v.IsActive ? "active" : "inactive")}")
    .ToList();

Console.WriteLine($"\nAll {summaryLines.Count} vendors:");

summaryLines.ForEach(
    line => Console.WriteLine($"  {line}"));

// 3. Search for one vendor
Vendor? found =
    vendors.FirstOrDefault(v => v.VendorCode == "VEN-999");

Console.WriteLine(
    $"\nVEN-999: {(found is null ? "not found" : found.Name)}");

//employees
var employees = new List<Employee>
{
    new Employee(1, "Priya Nair", "Senior Project Manager"),
    new Employee(2, "Arjun Mehta", "Project Manager"),
    new Employee(3, "Kavita Rao", "Site Engineer"),
};

var projects = new List<Project>
{
    new Project(1, "Riverside Substation", 1)
    {
        Status = ProjectStatus.Active
    },

    new Project(2, "Highway 47 Bridge", 2)
    {
        Status = ProjectStatus.Active
    },

    new Project(3, "Solar Park Phase 2", 1)
    {
        Status = ProjectStatus.Planned
    },

    new Project(4, "Metro Depot", 99)
{
    Status = ProjectStatus.Active
},
};

Console.WriteLine("\nActive vendors by category:");

var byCategory = vendors
    .Where(v => v.IsActive)
    .GroupBy(v => v.Category);

foreach (var group in byCategory)
{
    Console.WriteLine($"  {group.Key}: {group.Count()}");
}

Console.WriteLine("\nActive projects and managers:");

var projectManagers = projects
    .Where(p => p.Status == ProjectStatus.Active)
    .Join(
        employees,
        p => p.ManagerId,
        e => e.Id,
        (p, e) => $"  {p.Name} — managed by {e.Name}");

foreach (var line in projectManagers)
{
    Console.WriteLine(line);
}