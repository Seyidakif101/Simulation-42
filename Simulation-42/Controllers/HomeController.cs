using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simulation_42.Context;
using Simulation_42.ViewModels.ProjectViewModels;

namespace Simulation_42.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects.Select(x => new ProjectGetVM()
            {
                Id = x.Id,
                Name = x.Name,
                ImagePath = x.ImagePath,
                CategoryName = x.Category.Name
            }).ToListAsync();
            return View(projects);
        }
    }
}
