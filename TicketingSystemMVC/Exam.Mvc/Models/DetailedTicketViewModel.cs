using Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Exam.Mvc.Models
{
    public class DetailedTicketViewModel
    {
        public static Expression<Func<Ticket, DetailedTicketViewModel>> FromTicket
        {
            get
            {
                return ticket => new DetailedTicketViewModel
                {
                    Id = ticket.Id,
                    Title = ticket.Title,
                    CategoryName = ticket.Category.Name,
                    AuthorName = ticket.Author.UserName,
                    Comments = ticket.Comments.AsQueryable().Select(CommentViewModel.FromComment),
                    //Priority = ticket.Priority.ToString(),
                    ScreenshotUrl = ticket.ScreenshotUrl,
                    Description = ticket.Description
                };
            }
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryName { get; set; }

        public string AuthorName { get; set; }

        public string Priority { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public string ScreenshotUrl { get; set; }

        public string Description { get; set; }
    }
}