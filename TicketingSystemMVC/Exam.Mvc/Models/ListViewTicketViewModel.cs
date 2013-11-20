using Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Exam.Mvc.Models
{
    public class ListViewTicketViewModel
    {
        public static Expression<Func<Ticket, ListViewTicketViewModel>> FromTicket
        {
            get
            {
                return ticket => new ListViewTicketViewModel
                {
                    Id = ticket.Id,
                    Title = ticket.Title,
                    CategoryName = ticket.Category.Name,
                    AuthorName = ticket.Author.UserName,
                    Priority = ticket.Priority,
                };
            }
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryName { get; set; }

        public string AuthorName { get; set; }

        public Priority Priority { get; set; }

        public string PriorityString
        {
            get { return this.Priority.ToString(); }
        }
    }
}