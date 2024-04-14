using BookStore.Models;
using BookStore.Repository;
using BookStore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private IRepository db = new MyRepository();

        [HttpGet]
        public ActionResult Login()
        {
            return View(new Models.LoginViewModel());
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel credentials)
        {
            var users = db.GetUsers();

            if (ModelState.IsValid && users.Any(x => x.LOGIN == credentials.login && x.PASSWORD == credentials.password))
            {
                Utils.roleToSession(credentials.login);
                FormsAuthentication.RedirectFromLoginPage(credentials.login, true);
                return RedirectToAction("Index","Home");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}