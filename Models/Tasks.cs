using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseEF.Models
{
    public class Tasks
    {
        public Guid TasksId { get; set; }
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual Category Category { get; set; }
    }
}


