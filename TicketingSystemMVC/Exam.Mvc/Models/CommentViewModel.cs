using Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Exam.Mvc.Models
{
    public class CommentViewModel
    {
        public static Expression<Func<Comment, CommentViewModel>> FromComment
        {
            get
            {
                return comment => new CommentViewModel
                {
                    Id = comment.Id,
                    AuthorName = comment.Author.UserName,
                    Content = comment.Content
                };
            }
        }

        public int Id { get; set; }

        public string Content { get; set; }

        public string AuthorName { get; set; }

    }
}