using FastTypingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FastTypingApp
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<UserExercise> UserExercises { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseMySQL("server=127.0.0.1;database=fast_typing_app_db_2;uid=root;pwd='';");
        }
    }
}
