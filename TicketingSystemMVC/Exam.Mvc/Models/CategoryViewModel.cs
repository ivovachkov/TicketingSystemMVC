using Exam.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Exam.Mvc.Models
{
    public class CategoryViewModel
    {
        public static Expression<Func<Category, CategoryViewModel>> FromCategory
        {
            get
            {
                return cat => new CategoryViewModel
                {
                    Id = cat.Id,
                    Name = cat.Name
                };
            }
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}