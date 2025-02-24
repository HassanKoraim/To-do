namespace Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime UpdatedTime { get; set;} = DateTime.Now;
        public DateTime DueTime { get; set; }
        public int Status { get; set; }

    }
}
