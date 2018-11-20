using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    [Table("blogs")]
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public virtual TodoUser User { get; set; }
    }
}