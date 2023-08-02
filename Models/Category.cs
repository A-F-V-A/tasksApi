using System.Text.Json.Serialization;

namespace CourseEF.Models
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Difficulty { get; set; }
        [JsonIgnore]
        public virtual ICollection<Tasks> Tasks { get; set;}
    }
}

