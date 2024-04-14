using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using BookStore.Repository;
using BookStore.Util;
using BookStore.ViewModels;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private IRepository db = new MyRepository();

        [HttpGet]
        [CustomAuthorize(Role = "admin")]
        public ActionResult Add()
        {
            return View(new BookViewModel());
        }

        [HttpPost]
        [CustomAuthorize(Role = "admin")]
        public ActionResult Add(BookViewModel book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.AddBook(book.ToEntity(new BOOK()));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("error", "Ошибка при добавлении");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [CustomAuthorize(Role = "admin")]
        public ActionResult Edit(long id)
        {
            BOOK book = db.GetBooks().FirstOrDefault(x => x.ID == id);
            if (book != null)
            {
                return View("Add", book.ToViewModel(new BookViewModel()));
            }
            else
            {
                ModelState.AddModelError("error", "Элемент не найден в базе данных");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [CustomAuthorize(Role = "admin")]
        public ActionResult Edit(BookViewModel book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.EditBook(book.ToEntity(new BOOK()));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("error", "Ошибка при обновлении");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [CustomAuthorize(Role = "admin")]
        public ActionResult Delete(long id)
        {
            BOOK book = db.GetBooks().FirstOrDefault(x => x.ID == id);
            if (book != null)
            {
                try
                {
                    db.DeleteBook(book);
                }
                catch(Exception)
                {
                    ModelState.AddModelError("error", "Ошибка при удалении");
                }
            }
            else
            {
                ModelState.AddModelError("error", "Элемент не найден в базе данных");
            }
            return RedirectToAction("Index", "Home");
        }

        [CustomAuthorize]
        public ActionResult Take(long id)
        {
            try
            {
                db.TakeABook(User.Identity.Name, id);
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message });
            }
            return Json(new { msg = "OK" });
        }

        [CustomAuthorize]
        public ActionResult Return(long id)
        {
            try
            {
                db.ReturnABook(User.Identity.Name, id);
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message });
            }
            return Json(new { msg = "OK" });
        }
    }
}