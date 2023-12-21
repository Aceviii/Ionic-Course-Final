
using System.ComponentModel.DataAnnotations;

namespace task_api.Models
{
    public class Task
    {
        [Key]
        
        public int TaskId { get; set; }

        public string Title { get; set; }

        public bool Completed { get; set; }
    }
}
