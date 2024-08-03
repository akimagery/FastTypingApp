using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using FastTypingApp.Models;


namespace FastTypingApp.Controllers
{
    public class ExerciseAdminController : Controller
    {
        AppDbContext dbContext;

        public ExerciseAdminController(AppDbContext context)
        {
            dbContext = context;
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            return View(await dbContext.Exercises.ToListAsync());
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Exercise exercise)
        {
            dbContext.Exercises.Add(exercise);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Exercise? exercise = await dbContext.Exercises.FirstOrDefaultAsync(p => p.Id == id);
                if (exercise != null)
                {
                    dbContext.Exercises.Remove(exercise);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Exercise? exercise = await dbContext.Exercises.FirstOrDefaultAsync(p => p.Id == id);
                if (exercise != null) return View(exercise);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Exercise exercise)
        {
            dbContext.Exercises.Update(exercise);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
