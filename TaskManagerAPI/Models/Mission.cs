using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerAPI.Models
{
    public class Mission
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        [ForeignKey("Project")]
        public int ProjectId {  get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
    }
}
