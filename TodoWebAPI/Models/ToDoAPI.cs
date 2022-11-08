using System.ComponentModel.DataAnnotations;

namespace TodoWebAPI.Models
{
    public class ToDoAPI
    {
        public Guid Id { get; set; }
        public string Task { get; set; }
        public string Priority { get; set; }
        public string Category { get; set; }
    }
}
