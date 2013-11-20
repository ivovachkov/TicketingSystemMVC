using Exam.Mvc.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam.Models;

namespace Exam.Mvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesAdminController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create([DataSourceRequest] DataSourceRequest request, CategoryViewModel category)
        {
            var catToAdd = new Category { Name = category.Name };
            db.Categories.Add(catToAdd);
            db.SaveChanges();

            return Json(new[] { category }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var categories = db.Categories.All()
                .Select(CategoryViewModel.FromCategory);

            return Json(categories.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update([DataSourceRequest] DataSourceRequest request, CategoryViewModel category)
        {
            var catFromDb = db.Categories.GetById(category.Id);
            catFromDb.Name = category.Name;
            db.SaveChanges();

            return Json(new[] { category }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, CategoryViewModel category)
        {
            db.Categories.Delete(category.Id);
            db.SaveChanges();

            return Json(new[] { category }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}