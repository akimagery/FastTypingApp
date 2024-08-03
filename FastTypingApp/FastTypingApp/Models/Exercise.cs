using System.ComponentModel.DataAnnotations;

namespace FastTypingApp.Models
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        public int ErrorNum { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public int MaxTime { get; set; }
    }
}
