using System;
using System.ComponentModel.DataAnnotations;

namespace TodoListAPI.Models
{
    //Data transfer obcject class of creating new Todo entity
    public class CreateTodoDto
    {
        [Required]
        [MaxLength(32)]
        public string Title { get; set; }
        [Required]
        [MaxLength(3200)]
        public string Description { get; set; }
        public DateTime StartDate = DateTime.Now;
        [Required]
        public DateTime ExpireDate { get; set; }
        public byte PercentComplete = 0;
        public bool Done = false;
    }
}
