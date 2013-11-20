using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Models
{
    public class Ticket
    {
        public Ticket()
        {
            this.Priority = Priority.Medium;
            this.Comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringNotContains("bug")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The title should be between {2} and {1} symbols")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Author")]
        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        public Priority Priority { get; set; }

        [Display(Name = "Screenshot URL")]
        public string ScreenshotUrl { get; set; }

        public string Description { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
