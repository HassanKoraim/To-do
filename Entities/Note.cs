using Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        [Required]
        public DateTime DueTime { get; set; }
        public StatusOptions Status { get; set; } = StatusOptions.NotCompleted;

    }
}
