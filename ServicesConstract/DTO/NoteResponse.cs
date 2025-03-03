using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Entities;

namespace ServicesConstract.DTO
{
    public class NoteResponse
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The Title is Required")]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }
        [Required(ErrorMessage = "The Due Time is Required")]
        public DateTime? DueTime { get; set; }
        public StatusOptions Status { get; set; }
    }
    public static class MissionResponseExtension
    {
        public static NoteResponse ToNoteResponse( this Note mission)
        {
            return new NoteResponse()
            {
                Id = mission.Id,
                Title = mission.Title,
                Description = mission.Description,
                CreatedTime = mission.CreatedTime,
                DueTime = mission.DueTime,
                Status = mission.Status
            };
        }
    }
}
