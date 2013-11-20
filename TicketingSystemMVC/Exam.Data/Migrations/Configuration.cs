namespace Exam.Data.Migrations
{
    using Exam.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Exam.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Exam.Data.ApplicationDbContext context)
        {
            this.RegisterAdmin(context);
            this.PopulateTickets(context);            
        }

        private void PopulateTickets(Exam.Data.ApplicationDbContext context)
        {
            if (context.Tickets.Any())
            {
                return;
            }

            var user = new ApplicationUser { UserName = "pesho" };
            var rand = new Random();

            for (int i = 0; i < 20; i++)
            {
                var ticket = new Ticket
                {
                    Author = context.Users.FirstOrDefault(),
                    Priority = (Priority)rand.Next(0, 2),
                    Title = "Ticket number " + i,
                    Category = new Category { Name = "Category " + i },
                };

                for (int j = 0; j < rand.Next(1, 10); j++)
                {
                    ticket.Comments.Add(new Comment { Author = user, Content = "Comment " + j });
                }

                context.Tickets.Add(ticket);
            }

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            
        }

        private void RegisterAdmin(Exam.Data.ApplicationDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var db = new ApplicationDbContext();

            var userAdmin = new ApplicationUser()
            {
                UserName = "admin",
                Logins = new Collection<UserLogin>()
                {
                    new UserLogin()
                    {
                        LoginProvider = "Local",
                        ProviderKey = "admin",
                    }
                },
                Roles = new Collection<UserRole>()
                {
                    new UserRole()
                    {
                        Role = new Role("Admin")
                    }
                }
            };

            db.Users.Add(userAdmin);
            db.UserSecrets.Add(new UserSecret("admin",
                "ACQbq83L/rsvlWq11Zor2jVtz2KAMcHNd6x1SN2EXHs7VuZPGaE8DhhnvtyO10Nf5Q=="));//admin123
            db.SaveChanges();
        }
    }
}
