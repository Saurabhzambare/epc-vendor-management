namespace Warmup;

public class VendorRegistry
{
    public static async Task<List<Vendor>> LoadVendorsAsync()
    {
        await Task.Delay(500);

        return new List<Vendor>
        {
            new Vendor(
                "VEN-101",
                "Coastal Marine Works",
                VendorCategory.Civil),

            new Vendor(
                "VEN-102",
                "Trident Power Systems",
                VendorCategory.Electrical),
        };
    }
}


