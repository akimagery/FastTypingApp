using FastTypingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace FastTypingApp.Controllers
{
    public class HomeController: Controller
    {
        AppDbContext dbContext;

        public HomeController(AppDbContext context)
        {
            dbContext = context;
        }

        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> Index()
        {
            User? user = dbContext.Users.FirstOrDefault(u => u.Name == HttpContext.User.Identity.Name);
            ViewBag.User = user;

            List<UserExercise> userExercises = dbContext.UserExercises.Where(x => x.UserId == user.Id).ToList();
            ViewBag.UserExercises = userExercises;
            ViewBag.ExercisesNum = userExercises.ToList().Count();

            return View(await dbContext.Exercises.OrderBy(x => x.Level).ThenBy(x => x.Name).ToListAsync());
        }

        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> Exercise(int? id)
        {
            User? user = dbContext.Users.FirstOrDefault(u => u.Name == HttpContext.User.Identity.Name);
            ViewBag.User = user;

            if (id != null)
            {
                Exercise? exercise = await dbContext.Exercises.FirstOrDefaultAsync(p => p.Id == id);
                if (exercise != null)
                {
                    ViewBag.Exercise = exercise;
                    return View();
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> SaveExercise(UserExercise userExercise)
        {
            dbContext.UserExercises.Add(userExercise);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
