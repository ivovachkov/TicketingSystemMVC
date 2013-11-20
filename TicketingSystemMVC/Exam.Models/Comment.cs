using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Display(Name = "Author")]
        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        [Display(Name = "Ticket")]
        public int TicketId { get; set; }

        public Ticket Ticket { get; set; }
    }
}
