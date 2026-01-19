using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Simulation_42.Context;
using Simulation_42.Helper;
using Simulation_42.Models;
using Simulation_42.ViewModels.ProjectViewModels;

namespace Simulation_42.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class ProjectController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly string _folderPath;

        public ProjectController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _folderPath = Path.Combine(_environment.WebRootPath,"assets","images");
        }
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

        public async Task<IActionResult> Create()
        {
            await _sendCategoryViewBag();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateVM vm)
        {
            await _sendCategoryViewBag();
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var existCategory = await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId);
            if (!existCategory)
            {
                ModelState.AddModelError("CategoryId", "Bele bir Category Yoxdur");
                return View(vm);
            }
            if (!vm.Image.CheckSize(2))
            {
                ModelState.AddModelError("Image", "2mb boyuyk olmamalidi");
                return View(vm);

            }
            if (!vm.Image.CheckType("image"))
            {
                ModelState.AddModelError("Image", "Image tipinde olmalidi file");
                return View(vm);
            }
            string uniqueFileName = await vm.Image.FileUploadAsync(_folderPath);
            Project project = new()
            {
                Name=vm.Name,
                ImagePath=uniqueFileName,
                CategoryId=vm.CategoryId
            };
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if(project is null)
            {
                return NotFound();
            }
            ProjectUpdateVM vm = new()
            {
                Name = project.Name,
                CategoryId = project.CategoryId
            };
            await _sendCategoryViewBag();
            return View(vm);
        }
        public async Task<IActionResult> Update(ProjectUpdateVM vm)
        {
            await _sendCategoryViewBag();
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var existCategory = await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId);
            if (!existCategory)
            {
                ModelState.AddModelError("CategoryId", "Bele bir Category Yoxdur");
                return View(vm);
            }
            if (!vm.Image?.CheckSize(2) ?? false)
            {
                ModelState.AddModelError("Image", "2mb boyuyk olmamalidi");
                return View(vm);

            }
            if (!vm.Image?.CheckType("image") ?? false)
            {
                ModelState.AddModelError("Image", "Image tipinde olmalidi file");
                return View(vm);
            }
            var existProject = await _context.Projects.FindAsync(vm.Id);
            existProject.Name = vm.Name;
            existProject.CategoryId = vm.CategoryId;
            if(vm.Image is { })
            {
                string uniqueFileName = await vm.Image.FileUploadAsync(_folderPath);
                string oldFileName = Path.Combine(_folderPath, existProject.ImagePath);
                FileHelper.FileDelete(oldFileName);
                existProject.ImagePath = uniqueFileName;
            }
            _context.Projects.Update(existProject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if(project is null)
            {
                return NotFound();
            }
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            string deleteFileName = Path.Combine(_folderPath, project.ImagePath);
            FileHelper.FileDelete(deleteFileName);
            return RedirectToAction(nameof(Index));

        }
        private async Task _sendCategoryViewBag()
        {
            var categories = await _context.Categories.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToListAsync();
            ViewBag.Categories = categories;
        }
    }
}
