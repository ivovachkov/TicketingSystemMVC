using Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Exam.Mvc.Models
{
    public class TicketViewModel
    {
        public static Expression<Func<Ticket, TicketViewModel>> FromTicket
        {
            get
            {
                return ticket => new TicketViewModel 
                {
                    Id = ticket.Id,
                    Title = ticket.Title,
                    CategoryName = ticket.Category.Name,
                    AuthorName = ticket.Author.UserName,
                    NumberOfComments = ticket.Comments.Count()
                };
            }
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryName { get; set; }

        public string AuthorName { get; set; }

        public int NumberOfComments { get; set; }
    }
}