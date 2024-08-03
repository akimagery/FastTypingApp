using FastTypingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastTypingApp.Controllers
{
    public class UserExerciseAdminController : Controller
    {
        AppDbContext dbContext;

        public UserExerciseAdminController(AppDbContext context)
        {
            dbContext = context;
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Users = dbContext.Users.ToList();
            ViewBag.Exercises = dbContext.Exercises.ToList();

            List<UserExercise> userExercises = dbContext.UserExercises.OrderBy(x => x.User.Name).ThenBy(x => x.Exercise.Name).ToList();
            ViewBag.UserExercises = userExercises;
            ViewBag.ExercisesNum = userExercises.ToList().Count();
            return View(await dbContext.UserExercises.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                UserExercise? userExercise = await dbContext.UserExercises.FirstOrDefaultAsync(p => p.Id == id);
                if (userExercise != null)
                {
                    dbContext.UserExercises.Remove(userExercise);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
