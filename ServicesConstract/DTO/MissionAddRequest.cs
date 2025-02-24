using Entities;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesConstract.DTO
{
    public class MissionAddRequest
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

        public Mission ToMission()
        {
            return new Mission()
            { Id = Id, Title = Title, Description = Description, CreatedTime = CreatedTime, DueTime = DueTime, Status = Status };
        }
    }
}
