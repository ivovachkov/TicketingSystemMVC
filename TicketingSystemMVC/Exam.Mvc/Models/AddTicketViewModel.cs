using Exam.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exam.Mvc.Models
{
    public class AddTicketViewModel
    {
        [Required]
        [StringNotContains("bug", ErrorMessage = "The word 'bug' should not be used in the title!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The title should be between {2} and {1} symbols!")]
        public string Title { get; set; }
        
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        
        [Required]
        public Priority Priority { get; set; }
        
        [Display(Name="Screenshot URL")]
        public string ScreenshotUrl { get; set; }

        public string Description { get; set; }
    }
}