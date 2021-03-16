using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Posts")]
    public class Post : AuditableEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }

        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }

        public Post()
        {

        }

        public Post(int id, string title, string content)
        {
            (ID, Title, Content) = (id, title, content);
        }
    }
}
