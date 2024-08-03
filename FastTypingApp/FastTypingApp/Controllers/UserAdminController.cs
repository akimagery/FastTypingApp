using FastTypingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace FastTypingApp.Controllers
{
    public class UserAdminController : Controller
    {
        AppDbContext dbContext;

        public UserAdminController(AppDbContext context)
        {
            dbContext = context;
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            return View(await dbContext.Users.ToListAsync());
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            user.Password = HashUtils.sha256(user.Password);
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                User? user = await dbContext.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                {
                    dbContext.Users.Remove(user);
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
                User? user = await dbContext.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null) return View(user);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
