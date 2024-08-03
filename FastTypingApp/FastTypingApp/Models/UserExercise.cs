using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FastTypingApp.Models
{
    public class UserExercise
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }      
        public User? User { get; set; }
        [Required]
        public int ExerciseId { get; set; }
        public Exercise? Exercise { get; set; }
        [Required]
        public int ErrorNum { get; set; }
        [Required]
        public double Accuracy { get; set; }
        [Required]
        public double Time { get; set; }
        [Required]
        public bool Succefull { get; set; }
    }
}
