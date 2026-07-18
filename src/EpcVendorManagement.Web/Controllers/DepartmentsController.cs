using EpcVendorManagement.Web.Data;
using EpcVendorManagement.Web.Models.Entities;
using EpcVendorManagement.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpcVendorManagement.Web.Controllers;

public class DepartmentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public DepartmentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var departments = await _context.Departments
            .OrderBy(department => department.Name)
            .ToListAsync();

        return View(departments);
    }

    public async Task<IActionResult> Details(int id)
    {
        var department = await _context.Departments
            .FirstOrDefaultAsync(department => department.Id == id);

        if (department is null)
        {
            return NotFound();
        }

        return View(department);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var department = await _context.Departments
            .FirstOrDefaultAsync(department => department.Id == id);

        if (department is null)
        {
            return NotFound();
        }

        var model = new DepartmentEditViewModel
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description,
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, DepartmentEditViewModel model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var department = await _context.Departments
            .FirstOrDefaultAsync(department => department.Id == id);

        if (department is null)
        {
            return NotFound();
        }

        bool nameTaken = await _context.Departments
            .AnyAsync(department =>
                department.Name == model.Name &&
                department.Id != id);

        if (nameTaken)
        {
            ModelState.AddModelError(
                nameof(model.Name),
                "A department with this name already exists.");

            return View(model);
        }

        department.Name = model.Name;
        department.Description = model.Description;

        await _context.SaveChangesAsync();

        TempData["Success"] =
            $"Department '{department.Name}' updated.";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Deactivate(int id)
    {
        var department = await _context.Departments
            .FirstOrDefaultAsync(department => department.Id == id);

        if (department is null)
        {
            return NotFound();
        }

        department.IsActive = false;

        await _context.SaveChangesAsync();

        TempData["Success"] =
            $"Department '{department.Name}' deactivated.";

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Create()
    {
        return View(new DepartmentFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        DepartmentFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        bool nameTaken = await _context.Departments
            .AnyAsync(department =>
                department.Name == model.Name);

        if (nameTaken)
        {
            ModelState.AddModelError(
                nameof(model.Name),
                "A department with this name already exists.");

            return View(model);
        }

        var department = new Department
        {
            Name = model.Name,
            Description = model.Description,
        };

        _context.Departments.Add(department);
        await _context.SaveChangesAsync();

        TempData["Success"] =
            $"Department '{department.Name}' created.";

        return RedirectToAction(nameof(Index));
    }
}