using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using static System.Collections.Specialized.BitVector32;

namespace BookStore.Util
{
    public class Utils
    {
        public static void roleToSession(string login)
        {
            HttpContext.Current.Session.Add("admin", login == "admin" ? "Y" : "N");
        }
    }
}