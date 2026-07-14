using Warmup;

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