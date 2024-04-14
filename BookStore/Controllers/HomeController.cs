using BookStore.Models;
using BookStore.Repository;
using BookStore.Util;
using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private IRepository db = new MyRepository();
        public ActionResult Index()
        {
            List<BookViewModel> books = books = db.GetBooks().Select(x => x.ToViewModel(new BookViewModel())).ToList();
            
            if (User.Identity.IsAuthenticated)
            {
                if (Session["admin"] == null)
                    Utils.roleToSession(User.Identity.Name);

                if (Session["admin"].ToString() != "Y")
                {
                    foreach (var book in books)
                    {
                        book.taken = db.HasTheBook(User.Identity.Name, book.id);
                    }
                }
            }
            return View(books);
        }

        [CustomAuthorize(Role = "admin")]
        public ActionResult Users()
        {
            List<USER> users = db.GetUsers();
            return View(users);
        }
    }
}