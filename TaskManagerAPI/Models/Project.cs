using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerAPI.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string? Description { get; set; }
        public ICollection<Task>? TaskList { get; set; }

    }
}
