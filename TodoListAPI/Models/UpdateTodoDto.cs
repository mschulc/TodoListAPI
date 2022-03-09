using System;
using System.ComponentModel.DataAnnotations;

namespace TodoListAPI.Models
{
    //Data transfer obcject class of updating Todo entity
    public class UpdateTodoDto
    {
        [MaxLength(32)]
        public string Title { get; set; }
        [MaxLength(3200)]
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
