using Warmup;

var vendors = new List<Vendor>
{
    new Vendor(
        "VEN-001",
        "Apex Civil Works",
        VendorCategory.Civil),

    new Vendor(
        "VEN-002",
        "Bharat Electricals",
        VendorCategory.Electrical),

    new Vendor(
        "VEN-003",
        "Meridian IT Solutions",
        VendorCategory.ItServices),
};

vendors[1].IsActive = false;

vendors[2].ContactEmail =
    "sales@meridian-it.example";

foreach (var vendor in vendors)
{
    if (vendor.IsActive)
    {
        Console.WriteLine(
            $"{vendor.VendorCode}: " +
            $"{vendor.Name} " +
            $"({vendor.Category})");
    }
}