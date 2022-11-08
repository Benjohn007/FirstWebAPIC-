namespace TodoWebAPI.Models
{
    public class AddToDoRequest
    {
        public string Task { get; set; }
        public string Priority { get; set; }
        public string Category { get; set; }
    }
}
