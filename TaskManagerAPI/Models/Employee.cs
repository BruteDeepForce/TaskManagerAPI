using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Email { get; set; }
        public int Password { get; set; }
        public string Role { get; set; }

        [ForeignKey("Mission")]
        public int MissionId { get; set; }
    }
}
