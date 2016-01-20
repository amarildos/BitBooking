using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitBooking.MVC.Models
{
    public class Error
    {

        public class Err : HandleErrorAttribute
        {
            public override void OnException(ExceptionContext filterContext)
            {
                Exception ex = filterContext.Exception;
                filterContext.ExceptionHandled = true;
                var model = new HandleErrorInfo(filterContext.Exception, "Home", "Index");

                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary(model)
                };
            }
        }
    }
}