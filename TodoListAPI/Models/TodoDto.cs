using System;

namespace TodoListAPI.Models
{
    //Data transfer obcject class of Todo entity
    public class TodoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }
        public byte PercentComplete { get; set; }
        public bool Done { get; set; }
        public int ExpireDay { get; set; }
        public int ExpireMonth { get; set; }
        public int ExpireYear { get; set; }
    }
}
