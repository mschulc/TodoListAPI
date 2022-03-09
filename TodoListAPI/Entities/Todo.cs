using System;

namespace TodoListAPI.Entities
{
    // Todo class which is directly related to database.
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public byte PercentComplete { get; set; }
        public bool Done { get; set; }
        public int ExpireDay { get; set; }
        public int ExpireMonth { get; set; }
        public int ExpireYear { get; set; }
    }
}
