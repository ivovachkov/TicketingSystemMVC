using Exam.Models;
using Exam.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Net;

namespace Exam.Mvc.Controllers
{
    public class TicketsController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTickets([DataSourceRequest] DataSourceRequest request)
        {
            var tickets = db.Tickets.All().Select(ListViewTicketViewModel.FromTicket);

            return Json(tickets.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, 
                    "There is no selected ticket");
            }

            var ticketFromDb = db.Tickets.All()
                .Where(x => x.Id == id);

            var ticket = ticketFromDb
                .Select(DetailedTicketViewModel.FromTicket)
                .FirstOrDefault();

            ticket.Priority = ticketFromDb.FirstOrDefault().Priority.ToString();

            return View(ticket);
        }

        [Authorize]
        public ActionResult AddTicket()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.All(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddTicket(AddTicketViewModel ticketVM)
        {
            var userId = User.Identity.GetUserId();
            var ticket = new Ticket
            {
                AuthorId = userId,
                Title = ticketVM.Title,
                CategoryId = ticketVM.CategoryId,
                Priority = ticketVM.Priority,
                ScreenshotUrl = ticketVM.ScreenshotUrl,
                Description = ticketVM.Description
            };

            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                var user = db.Users.All().FirstOrDefault(x => x.Id == userId);
                user.Points++;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories.All(), "Id", "Name", ticket.CategoryId);
            return View(ticket);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(SubmitCommentModel commentModel)
        {
            if (ModelState.IsValid)
            {
                var username = this.User.Identity.GetUserName();
                var userId = this.User.Identity.GetUserId();

                db.Comments.Add(new Comment
                {
                    AuthorId = userId,
                    Content = commentModel.Comment,
                    TicketId = commentModel.TicketId,
                });

                db.SaveChanges();

                var viewModel = new CommentViewModel { AuthorName = username, Content = commentModel.Comment };

                return PartialView("_Comment", viewModel);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
        }

        public ActionResult GetCategories()
        {
            var result = db.Categories
                .All()
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name
                });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(int? categorySearch)
        {
            var tickets = db.Tickets.All();

            if (categorySearch != null)
            {
                tickets = tickets.Where(x => x.CategoryId == categorySearch);
            }

            var result = tickets.Select(TicketViewModel.FromTicket);

            return View(result);
        }
    }
}