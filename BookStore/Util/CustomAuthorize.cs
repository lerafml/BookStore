using BookStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Util
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        public string Role { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                return false;

            if (Role == "admin" && httpContext.User.Identity.Name != "admin")
            {
                return false;
            }
            else
            {
                return base.AuthorizeCore(httpContext);
            }
        }
    }
}