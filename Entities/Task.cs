using Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime UpdatedTime { get; set;} = DateTime.Now;
        [Required]
        public DateTime DueTime { get; set; }
        public StatusOptions Status { get; set; } = StatusOptions.NotCompleted;

    }
}
