using BookStore.Models;
using BookStore.Repository;
using BookStore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    [CustomAuthorize(Role = "admin")]
    public class UsersController : Controller
    {
        private IRepository db = new MyRepository();

        [HttpGet]
        public ActionResult Add()
        {
            return View(new USER());
        }

        [HttpPost]
        public ActionResult Add(USER user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.AddUser(user);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("error", "Ошибка при добавлении");
                }
            }
            return RedirectToAction("Users", "Home");
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            USER user = db.GetUsers().FirstOrDefault(x => x.ID == id);
            if (user != null)
            {
                return View("Add", user);
            }
            else
            {
                ModelState.AddModelError("error", "Элемент не найден в базе данных");
                return RedirectToAction("Users", "Home");
            }
        }

        [HttpPost]
        public ActionResult Edit(USER user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.EditUser(user);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("error", "Ошибка при обновлении");
                }
            }
            return RedirectToAction("Users", "Home");
        }

        [HttpGet]
        public ActionResult Delete(long id)
        {
            USER user = db.GetUsers().FirstOrDefault(x => x.ID == id);
            if (user != null)
            {
                try
                {
                    db.EditUser(user);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("error", "Ошибка при удалении");
                }
            }
            else
            {
                ModelState.AddModelError("error", "Элемент не найден в базе данных");
            }
            return RedirectToAction("Users", "Home");
        }
    }
}