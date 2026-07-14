namespace Warmup;

public enum VendorCategory
{
    Civil,
    Electrical,
    Mechanical,
    ItServices
}

public class Vendor
{
    public string VendorCode { get; }
    public string Name { get; set; }
    public VendorCategory Category { get; set; }
    public string? ContactEmail { get; set; }
    public bool IsActive { get; set; } = true;

    public Vendor(
        string vendorCode,
        string name,
        VendorCategory category)
    {
        VendorCode = vendorCode;
        Name = name;
        Category = category;
    }
}