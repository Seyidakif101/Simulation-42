using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Simulation_42.Context;
using Simulation_42.Helper;
using Simulation_42.Models;
using Simulation_42.ViewModels.CategoryViewModels;

namespace Simulation_42.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.Select(x => new CategoryGetVM()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            Category category = new()
            {
                Name = vm.Name,
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is null)
            {
                return NotFound();
            }
            CategoryUpdateVM vm = new()
            {
                Name = category.Name
            };
            return View(vm);
        }
        public async Task<IActionResult> Update(CategoryUpdateVM vm)
        {
            
            var existCategory = await _context.Categories.FindAsync(vm.Id);
            existCategory.Name = vm.Name;
            
            _context.Categories.Update(existCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));

        }
        
    }
}
