using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; 

namespace TodoApi.Models
{
    [Table("users")]
    public class TodoUser
    {
        public int Id { get; set; }

        public string Email { get; set; } 

        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }
     
}