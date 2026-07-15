using Warmup;

namespace Warmup.Tests;

public class VendorTests
{
    [Fact]
    public void NewVendor_IsActiveByDefault()
    {
        // Arrange + Act
        var vendor = new Vendor(
            "VEN-900",
            "Test Vendor",
            VendorCategory.Civil);

        // Assert
        Assert.True(vendor.IsActive);
    }

    [Fact]
    public void Deactivate_SetsIsActiveFalse()
    {
        // Arrange
        var vendor = new Vendor(
            "VEN-901",
            "Test Vendor",
            VendorCategory.Civil);

        // Act
        vendor.Deactivate();

        // Assert
        Assert.False(vendor.IsActive);
    }

    [Fact]
    public async Task LoadVendorsAsync_ReturnsVendors()
    {
        // Act
        var vendors =
            await VendorRegistry.LoadVendorsAsync();

        // Assert
        Assert.NotEmpty(vendors);

        Assert.All(
            vendors,
            vendor => Assert.True(vendor.IsActive));
    }
}