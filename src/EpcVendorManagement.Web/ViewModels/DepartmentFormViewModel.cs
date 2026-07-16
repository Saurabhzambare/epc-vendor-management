using System.ComponentModel.DataAnnotations;

namespace EpcVendorManagement.Web.ViewModels;

public class DepartmentFormViewModel
{
    [Required(ErrorMessage = "Department name is required.")]
    [StringLength(100)]
    [Display(Name = "Department name")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }
}